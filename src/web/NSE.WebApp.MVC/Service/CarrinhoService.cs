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

        public CarrinhoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.CarrinhoUrl);
        }

        public async Task<CarrinhoViewModel> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync("/carrinho");

            TratarErrosResponse(response);

            return await DeserializarObjectResponse<CarrinhoViewModel>(response);
        }
       

        public async Task<ResponseResult> AdicionarItemCarrinho(ItemProdutoViewModel produto)
        {
            var itemContent = SerializarConteudo(produto);
            var response = await _httpClient.PostAsync("/carrinho",itemContent);

            TratarErrosResponse(response);

            if(!TratarErrosResponse(response))
            {
                return await DeserializarObjectResponse<ResponseResult>(response);
            }

            return RetornoOk();
        }

        public async Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemProdutoViewModel produto)
        {
            var itemContent = SerializarConteudo(produto);
            var response = await _httpClient.PutAsync($"/carrinho/{produto.ProdutoId}", itemContent);

            TratarErrosResponse(response);

            if (!TratarErrosResponse(response))
            {
                return await DeserializarObjectResponse<ResponseResult>(response);
            }

            return RetornoOk();
        }


        public async Task<ResponseResult> RemoverItemCarrinho(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"/carrinho/{produtoId}");

            TratarErrosResponse(response);

            if (!TratarErrosResponse(response))
            {
                return await DeserializarObjectResponse<ResponseResult>(response);
            }

            return RetornoOk();
        }
    }
}
