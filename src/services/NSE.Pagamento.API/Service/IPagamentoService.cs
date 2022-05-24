using NSE.Core.Messages.Integration;
using System;
using System.Threading.Tasks;

namespace NSE.Pagamento.API.Service
{
    public interface IPagamentoService
    {
        Task<ResponseMessage> AutorizarPagamento(Models.Pagamento pagamento);
        Task<ResponseMessage> CapturarPagamento(Guid pedidoId);
        Task<ResponseMessage> CancelarPagamento(Guid pedidoId);
    }
}
