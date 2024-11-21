using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCU;
using ODP.Web.UI.Models.ViewModels.Tribunal.TCU;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Tribunal.TCU
{
    public class ContaIrregularService : Service, IContaIrregularService
    {
        private readonly HttpClient _httpClient;

        public ContaIrregularService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<ContaIrregularViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/contairregular/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<ContaIrregularViewModel>>(response);
        }

        public async Task<ContaIrregularViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/contairregular/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ContaIrregularViewModel>(response);
        }

        public async Task<List<ContaIrregularViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/contairregular/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<ContaIrregularViewModel>();
                }

                var tcu = await DeserializarObjetoResponse<List<ContaIrregularViewModel>>(response);
                return tcu;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<ContaIrregularViewModel>();
            }
        }

        public async Task<List<ContaIrregularViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/contairregular/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<ContaIrregularViewModel>();
                }

                var tcu = await DeserializarObjetoResponse<List<ContaIrregularViewModel>>(response);
                return tcu;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<ContaIrregularViewModel>();
            }
        }




    }
}
