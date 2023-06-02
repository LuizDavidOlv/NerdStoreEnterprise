using NSE.Core.Messages.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.MessageBus
{
    public interface IKafkaBus : IDisposable
    {
        Task ProducerAsync<T>(string topic, T message) where T : IntegrationEvent;
        Task ConsumerAsync<T>(string topic, Func<T, Task> onMessage, CancellationToken cancellation) where T : IntegrationEvent;
    }
}
