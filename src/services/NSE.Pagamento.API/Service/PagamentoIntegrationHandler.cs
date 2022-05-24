using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Core.Messages.Integration;
using NSE.MessageBus;
using NSE.Pagamento.API.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Pagamento.API.Service
{
    public class PagamentoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public PagamentoIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<PedidoIniciadoIntegrationEvent, ResponseMessage>(async request => await AutorizarPagamento(request));
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<PedidoCanceladoIntegrationEvent>("PedidoCancelado", async request => await CancelarPagamento(request));
            _bus.SubscribeAsync<PedidoBaixadoEstoqueIntegrationEvent>("PedidoBaixadoEstoque", async request => await CapturarPagamento(request));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            SetSubscribers();
            return Task.CompletedTask;
        }

        private async Task<ResponseMessage> AutorizarPagamento(PedidoIniciadoIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var pagamentoService = scope.ServiceProvider.GetRequiredService<IPagamentoService>();
            var pagamento = new Models.Pagamento
            {
                PedidoId = message.PedidoId,
                TipoPagamento = (TipoPagamento)message.TipoPagamento,
                Valor = message.Valor,
                CartaoCredito = new CartaoCredito(
                    message.NomeCartao, message.NumeroCartao, message.MesAnoVencimento, message.Cvv)
            };

            var response = await pagamentoService.AutorizarPagamento(pagamento);
        }

        private async Task CancelarPagamento(PedidoCanceladoIntegrationEvent message)
        {
            throw new System.NotImplementedException();
        }

        private async Task CapturarPagamento(PedidoBaixadoEstoqueIntegrationEvent message)
        {
            throw new System.NotImplementedException();
        }
    }
}
