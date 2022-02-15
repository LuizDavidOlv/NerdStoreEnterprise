using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Service
{
    public class CarrinhoService : Service, ICarrinhoService
    {
        private readonly HttpClient _httpClient;

        public async Task<CarrinhoViewModel> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync("/carrinho/");

            TratarErrosResponse(response);

            return await DeserializarObjectResponse<CarrinhoViewModel>(response);
        }
        public CarrinhoService(HttpClient httpClient,IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
        }

        public Task<ResponseResult> AdicionarItemCarrinho(ItemProdutoViewModel produto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemProdutoViewModel produto)
        {
            throw new NotImplementedException();
        }


        public Task<ResponseResult> RemoverItemCarrinho(Guid produtoId)
        {
            throw new NotImplementedException();
        }
    }
}
