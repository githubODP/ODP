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
    public class FornecedorPartidoService : Service, IFornecedorPartidoService
    {
        private readonly HttpClient _httpClient;

        public FornecedorPartidoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<FornecedorPartidoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/fornecedorpartido/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<FornecedorPartidoViewModel>>(response);
        }

        public async Task<FornecedorPartidoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/fornecedorpartido/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<FornecedorPartidoViewModel>(response);
        }


        public async Task<List<FornecedorPartidoViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/fornecedorpartido/consultacnpj/{cnpj}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<FornecedorPartidoViewModel>();
                }

                var tse = await DeserializarObjetoResponse<List<FornecedorPartidoViewModel>>(response);
                return tse;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<FornecedorPartidoViewModel>();
            }
        }

        public async Task<List<FornecedorPartidoViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/fornecedorpartido/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<FornecedorPartidoViewModel>();
                }

                var tse = await DeserializarObjetoResponse<List<FornecedorPartidoViewModel>>(response);
                return tse;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<FornecedorPartidoViewModel>();
            }
        }

    }
}
