using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using ODP.Web.UI.Models.ViewModels.GovernoFederal;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System;

namespace ODP.Web.UI.Services.GovernoFederal
{
    public class CnepService : Service, ICnepService
    {
        private readonly HttpClient _httpClient;

        public CnepService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<CnepViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/cnep/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<CnepViewModel>>(response);
        }

        public async Task<CnepViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/cnep/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CnepViewModel>(response);
        }



        public async Task<List<CnepViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/cnep/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<CnepViewModel>();
                }

                var cnep = await DeserializarObjetoResponse<List<CnepViewModel>>(response);
                return cnep;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<CnepViewModel>();
            }
        }

        public async Task<List<CnepViewModel>> BuscarCPF(string cpf)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/cnep/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<CnepViewModel>();
                }

                var cnep = await DeserializarObjetoResponse<List<CnepViewModel>>(response);
                return cnep;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<CnepViewModel>();
            }
        }
    }
}
