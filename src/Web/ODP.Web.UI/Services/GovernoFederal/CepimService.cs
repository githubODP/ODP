using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using ODP.Web.UI.Models.ViewModels.GovernoFederal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.GovernoFederal
{
    public class CepimService : Service, ICepimService
    {
        private readonly HttpClient _httpClient;

        public CepimService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<CepimViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/cepim/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<CepimViewModel>>(response);
        }

        public async Task<CepimViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/cepim/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CepimViewModel>(response);
        }



        public async Task<List<CepimViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/cepim/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<CepimViewModel>();
                }

                var cepim = await DeserializarObjetoResponse<List<CepimViewModel>>(response);
                return cepim;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<CepimViewModel>();
            }
        }

        public Task<List<CepimViewModel>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
