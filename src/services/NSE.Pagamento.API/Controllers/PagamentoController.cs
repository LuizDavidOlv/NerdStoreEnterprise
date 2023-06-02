using Microsoft.AspNetCore.Mvc;
using NSE.Core.Messages.Integration;
using NSE.Pagamento.API.Models;
using NSE.Pagamento.API.Service;
using NSE.Pagamento.NerdsPag;
using NSE.WebApi.Core.Controllers;
using System.Threading.Tasks;

namespace NSE.Pagamento.API.Controllers
{
    public class PagamentoController : MainController
    {
        [HttpPost("/pagamento/pedido-iniciado")]
        public async Task<IActionResult> PostAsync(PedidoIniciadoIntegrationEvent pedidoIniciadoIntegrationEvent,
           [FromServices] IPagamentoService pagamentoService)
        {
            var pagamento = ObterPagamento(pedidoIniciadoIntegrationEvent);
            var response = await pagamentoService.AutorizarPagamento(pagamento);

            return Ok(response);
        }

        private static Models.Pagamento ObterPagamento(PedidoIniciadoIntegrationEvent pedidoIniciadoIntegrationEvent)
        {
            return new Models.Pagamento
            {
                PedidoId = pedidoIniciadoIntegrationEvent.PedidoId,
                TipoPagamento = (TipoPagamento)pedidoIniciadoIntegrationEvent.TipoPagamento,
                Valor = pedidoIniciadoIntegrationEvent.Valor,
                CartaoCredito = new CartaoCredito
                (
                    pedidoIniciadoIntegrationEvent.NomeCartao,
                    pedidoIniciadoIntegrationEvent.NumeroCartao,
                    pedidoIniciadoIntegrationEvent.MesAnoVencimento,
                    pedidoIniciadoIntegrationEvent.Cvv
                )
            };
        }
    }
}
