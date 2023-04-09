using MediatR;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Pedidos.API.Application.Events
{
    public class PedidoEventHandler : INotificationHandler<PedidoRealizadoEvent>
    {
        //private readonly IMessageBus _bus;
        private readonly IKafkaBus _bus;

        public PedidoEventHandler(IKafkaBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(PedidoRealizadoEvent message, CancellationToken cancellationToken)
        {
            await _bus.ProducerAsync("PedidoRealizado",new PedidoRealizadoIntegrationEvent(message.ClienteId));
        }
    }
}
