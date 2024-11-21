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
    public class DoacaoCandidatoService : Service, IDoacaoCandidatoService
    {
        private readonly HttpClient _httpClient;

        public DoacaoCandidatoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<DoacaoCandidatoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/doacaocandidato/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DoacaoCandidatoViewModel>>(response);
        }

        public async Task<DoacaoCandidatoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/doacaocandidato/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DoacaoCandidatoViewModel>(response);
        }

        public async Task<List<DoacaoCandidatoViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/doacaocandidato/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DoacaoCandidatoViewModel>();
                }

                var tse = await DeserializarObjetoResponse<List<DoacaoCandidatoViewModel>>(response);
                return tse;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DoacaoCandidatoViewModel>();
            }
        }

        public async Task<List<DoacaoCandidatoViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/doacaocandidato/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DoacaoCandidatoViewModel>();
                }

                var tse = await DeserializarObjetoResponse<List<DoacaoCandidatoViewModel>>(response);
                return tse;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DoacaoCandidatoViewModel>();
            }
        }


    }
}
