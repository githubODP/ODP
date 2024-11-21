using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Dividas;
using ODP.Web.UI.Models.ViewModels.Dividas;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Dividas
{
    public class DividaFGTSService : Service, IDividaFGTsService
    {
        private readonly HttpClient _httpClient;

        public DividaFGTSService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<DividaFGTSViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/dividafgts/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DividaFGTSViewModel>>(response);
        }

        public async Task<DividaFGTSViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/dividafgts/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DividaFGTSViewModel>(response);
        }



        public async Task<List<DividaFGTSViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/dividafgts/consultacnpj/{cnpj}");
                TratarErrosResponse(response);

                // 
                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DividaFGTSViewModel>();
                }

                var divida = await DeserializarObjetoResponse<List<DividaFGTSViewModel>>(response);
                return divida;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DividaFGTSViewModel>();
            }
        }

        public async Task<List<DividaFGTSViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/dividafgts/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DividaFGTSViewModel>();
                }

                var divida = await DeserializarObjetoResponse<List<DividaFGTSViewModel>>(response);
                return divida;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DividaFGTSViewModel>();
            }
        }



    }
}
