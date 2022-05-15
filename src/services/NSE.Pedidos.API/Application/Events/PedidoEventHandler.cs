using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Pedidos.API.Application.Events
{
    public class PedidoEventHandler
    {
        private readonly IMessageBus _bus;

        public PedidoEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public async Task Handler(PedidoRealizadoEvent message, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(new PedidoRealizadoIntegrationEvent(message.ClienteId));
        }
    }
}
