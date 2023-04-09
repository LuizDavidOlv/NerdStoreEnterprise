using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Core.DomainObjects;
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
        //private readonly IMessageBus _bus;
        private readonly IKafkaBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public PagamentoIntegrationHandler(IKafkaBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<PedidoIniciadoIntegrationEvent, ResponseMessage>(async request => await AutorizarPagamento(request));
        }

        private void SetSubscribers(CancellationToken stoppingToken)
        {
            _bus.ConsumerAsync<PedidoCanceladoIntegrationEvent>("PedidoCancelado", async request => await CancelarPagamento(request), stoppingToken);
            _bus.ConsumerAsync<PedidoBaixadoEstoqueIntegrationEvent>("PedidoBaixadoEstoque", async request => await CapturarPagamento(request), stoppingToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            SetSubscribers(stoppingToken);
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

            return response;
        }

        private async Task CancelarPagamento(PedidoCanceladoIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var pagamentoService = scope.ServiceProvider.GetRequiredService<IPagamentoService>();
                var response = await pagamentoService.CancelarPagamento(message.PedidoId);

                if(!response.ValidationResult.IsValid)
                {
                    throw new CannotUnloadAppDomainException($"Falha ao cancelar pagamento do pedido{message.PedidoId}");
                }
            }
        }

        private async Task CapturarPagamento(PedidoBaixadoEstoqueIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var pagamentoService = scope.ServiceProvider.GetRequiredService<IPagamentoService>();
                var response = await pagamentoService.CapturarPagamento(message.PedidoId);

                if(!response.ValidationResult.IsValid)
                {
                    throw new DomainException($"Falha ao capturar pagamento do pedido {message.PedidoId}");
                }

                await _bus.ProducerAsync("PedidoPago",new PedidoPagoIntegrationEvent(message.ClienteId, message.PedidoId));
            }
        }
    }
}
