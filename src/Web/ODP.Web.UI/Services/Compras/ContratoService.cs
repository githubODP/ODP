using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Compras;
using ODP.Web.UI.Models.ViewModels.Compras;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Compras
{
    public class ContratoService : Service, IContratoService
    {
        private readonly HttpClient _httpClient;

        public ContratoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);


            _httpClient = httpClient;

        }



        public async Task<PagedResult<ContratoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/contratos/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<ContratoViewModel>>(response);
        }

        public async Task<ContratoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/contratos/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ContratoViewModel>(response);
        }



        public async Task<List<ContratoViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/contratos/consultacnpj/{cnpj}");
                TratarErrosResponse(response);

                // Verificar se a resposta contém conteúdo
                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<ContratoViewModel>(); // Retorna um modelo vazio
                }

                var contrato = await DeserializarObjetoResponse<List<ContratoViewModel>>(response);
                return contrato;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Retornar um modelo vazio se o contrato não for encontrado (404)
                return new List<ContratoViewModel>();
            }
        }

        public async Task<List<ContratoViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/contratos/consultacpf/{cpf}");
                TratarErrosResponse(response);

                // Verificar se a resposta contém conteúdo
                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<ContratoViewModel>(); // Retorna um modelo vazio
                }

                var contrato = await DeserializarObjetoResponse<List<ContratoViewModel>>(response);
                return contrato;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Retornar um modelo vazio se o contrato não for encontrado (404)
                return new List<ContratoViewModel>();
            }
        }

        

        
    }
}
