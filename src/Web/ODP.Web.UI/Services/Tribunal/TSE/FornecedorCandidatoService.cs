using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Tribunal.TSE;
using ODP.Web.UI.Models.ViewModels.Tribunal.TSE;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Tribunal.TSE
{
    public class FornecedorCandidatoService : Service, IFornecedorCandidatoService
    {
        private readonly HttpClient _httpClient;

        public FornecedorCandidatoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<FornecedorCandidatoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/fornecedorcandidato/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<FornecedorCandidatoViewModel>>(response);
        }

        public async Task<FornecedorCandidatoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/fornecedorcandidato/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<FornecedorCandidatoViewModel>(response);
        }

        public async Task<List<FornecedorCandidatoViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/fornecedorcandidato/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<FornecedorCandidatoViewModel>();
                }

                var tse = await DeserializarObjetoResponse< List<FornecedorCandidatoViewModel>>(response);
                return tse;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<FornecedorCandidatoViewModel>();
            }
        }

        public async Task<List<FornecedorCandidatoViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/fornecedorcandidato/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<FornecedorCandidatoViewModel>();
                }

                var tse = await DeserializarObjetoResponse< List<FornecedorCandidatoViewModel>>(response);
                return tse;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<FornecedorCandidatoViewModel>();
            }
        }
    }
}
