using Confluent.Kafka;
using NSE.Core.Messages.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.MessageBus
{
    public class KafkaBus : IKafkaBus
    {
        private readonly string _bootstrapserver;

        public KafkaBus(string bootstrapserver)
        {
            _bootstrapserver = bootstrapserver;
        }

        public async Task ProducerAsync<T>(string topic, T message) where T : IntegrationEvent
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapserver,
            };

            var payload = System.Text.Json.JsonSerializer.Serialize(message);

            var producer = new ProducerBuilder<string, string>(config).Build();

            var result = await producer.ProduceAsync(topic, new Message<string, string>
            {
                Key = Guid.NewGuid().ToString(),
                Value = payload
            });

            await Task.CompletedTask;
        }

        public async Task ConsumerAsync<T>(
            string topic,
            Func<T, Task> onMessage,
            CancellationToken cancellation) where T : IntegrationEvent
        {
            _ = Task.Factory.StartNew(async () =>
            {
                var config = new ConsumerConfig
                {
                    GroupId = "grupo-curso",
                    BootstrapServers = _bootstrapserver,
                    EnableAutoCommit = false,
                    EnablePartitionEof = true,
                };

                using var consumer = new ConsumerBuilder<string, string>(config).Build();
                consumer.Subscribe(topic);

                while (!cancellation.IsCancellationRequested)
                {
                    var result = consumer.Consume();

                    if(result.IsPartitionEOF)
                    {
                        continue;
                    }

                    var message = System.Text.Json.JsonSerializer.Deserialize<T>(result.Message.Value);
                    await onMessage(message);
                    consumer.Commit();
                }
            }, cancellation, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            await Task.CompletedTask; 
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
