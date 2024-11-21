using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCE;
using ODP.Web.UI.Models.ViewModels.Tribunal.TCE;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Tribunal.TCE
{
    public class CNPJRestritoService : Service, ICNPJRestritoService
    {
        private readonly HttpClient _httpClient;

        public CNPJRestritoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<CNPJRestritoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/cnpjrestrito/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<CNPJRestritoViewModel>>(response);
        }

        public async Task<CNPJRestritoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/cnpjrestrito/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CNPJRestritoViewModel>(response);
        }



        public async Task<List<CNPJRestritoViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/cnpjrestrito/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<CNPJRestritoViewModel>();
                }

                var tce = await DeserializarObjetoResponse<List<CNPJRestritoViewModel>>(response);
                return tce;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<CNPJRestritoViewModel>();
            }
        }

        public Task<List<CNPJRestritoViewModel>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
