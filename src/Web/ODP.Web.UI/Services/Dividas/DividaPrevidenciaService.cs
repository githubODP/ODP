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
    public class DividaPrevidenciaService : Service, IDividaPrevidenciaService
    {
        private readonly HttpClient _httpClient;

        public DividaPrevidenciaService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<DividaPrevidenciariaViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/dividaprevidenciarias/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DividaPrevidenciariaViewModel>>(response);
        }

        public async Task<DividaPrevidenciariaViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/dividaprevidenciarias/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DividaPrevidenciariaViewModel>(response);
        }



        public async Task<List<DividaPrevidenciariaViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);

            try
            {
                var response = await _httpClient.GetAsync($"api/dividaprevidenciarias/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DividaPrevidenciariaViewModel>();
                }

                var divida = await DeserializarObjetoResponse<List<DividaPrevidenciariaViewModel>>(response);
                return divida;
            }

            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DividaPrevidenciariaViewModel>();
            }
        }

        public async Task<List<DividaPrevidenciariaViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);

            try
            {
                var response = await _httpClient.GetAsync($"api/dividaprevidenciarias/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DividaPrevidenciariaViewModel>();
                }

                var divida = await DeserializarObjetoResponse<List<DividaPrevidenciariaViewModel>>(response);
                return divida;
            }

            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DividaPrevidenciariaViewModel>();
            }
        }




    }
}
