using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Service
{
    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;

        public CatalogoService(HttpClient httpClient,IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);
            _httpClient = httpClient;
        }

        public IOptions<AppSettings> Settings { get; }



        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            var response = _httpClient.GetAsync($"/catalogo/produtos/{id}");
            TratarErrosResponse(await response);
            return await DeserializarObjectResponse<ProdutoViewModel>(await response);
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            var response = _httpClient.GetAsync("/catalogo/produtos/");
            TratarErrosResponse(await response);
            return await DeserializarObjectResponse<IEnumerable<ProdutoViewModel>>(await response);
        }
    }
}
