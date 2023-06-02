using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.AspNetCore.Mvc;
using NSE.Core.Messages.Integration;
using NSE.MessageBus.Serializador;
using System;
using System.Collections.Generic;
using System.Linq;
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
                //This option is used to send messages in order
                EnableIdempotence = true,
                MaxInFlight = 1,
                MessageSendMaxRetries = 2,
                Acks = Acks.All,
                //
                //Acks = Acks.Leader,
                BootstrapServers = _bootstrapserver,
            };

            //var payload = System.Text.Json.JsonSerializer.Serialize(message);

            CreateTopicIfNecessary(topic);

            var producer = new ProducerBuilder<string, T>(config)
                .SetValueSerializer(new SerializerNSE<T>())
                .Build();

            var headers = new Headers();
            headers.Add("application", Encoding.UTF8.GetBytes("payment"));
            headers.Add("application", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));


            var result = await producer.ProduceAsync(topic, new Message<string, T>
            {
                Key = Guid.NewGuid().ToString(),
                Value = message,
                Headers = headers,
            });

            //This option is used to commit manually
            producer.CommitTransaction();
            //This option is used to abort manually
                //producer.AbortTransaction();

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
                    //This option is used to read all messages from the beginning
                        //AutoOffsetReset = AutoOffsetReset.Earliest,
                    //This option is used to read only new messages
                        //AutoOffsetReset = AutoOffsetReset.Latest,
                    GroupId = "grupo-curso",
                    BootstrapServers = _bootstrapserver,
                    EnableAutoCommit = false,
                    EnablePartitionEof = true,
                    //This option is used to commit manually
                    EnableAutoOffsetStore = false,
                    IsolationLevel = IsolationLevel.ReadCommitted,
                };

                using var consumer = new ConsumerBuilder<string, T>(config)
                     .SetValueDeserializer(new DeserializerNSE<T>())
                     .Build();

                consumer.Subscribe(topic);

                while (!cancellation.IsCancellationRequested)
                {
                    var result = consumer.Consume();

                    if(result.IsPartitionEOF)
                    {
                        continue;
                    }

                    var headers = result.Message.Headers
                    .ToDictionary(p => p.Key, p => Encoding.UTF8.GetString(p.GetValueBytes()));

                    var application = headers["application"];
                    var transactionId = headers["transactionId"];

                    //var message = System.Text.Json.JsonSerializer.Deserialize<T>(result.Message.Value);
                    await onMessage(result.Message.Value);
                    consumer.Commit();
                }
            }, cancellation, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            await Task.CompletedTask; 
        }

        private async void CreateTopicIfNecessary(string topic)
        {
            var adminClientConfig = new AdminClientConfig
            {
                BootstrapServers = _bootstrapserver
            };

            using (var adminClient = new AdminClientBuilder(adminClientConfig).Build())
            {
                try
                {
                    await adminClient.CreateTopicsAsync(new TopicSpecification[] {
                new TopicSpecification { Name = topic, ReplicationFactor = 1, NumPartitions = 1 } });
                }
                catch (CreateTopicsException e)
                {
                    // Topic already exists
                    Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
                }
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
