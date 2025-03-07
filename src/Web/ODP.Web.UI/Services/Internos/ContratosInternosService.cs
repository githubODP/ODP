using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Internos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Internos
{
    public class ContratosInternosService : Service, IContratosInternosService
    {
        private readonly HttpClient _httpClient;

        public ContratosInternosService(HttpClient httpClient,
          IOptions<AppSettings> settings)
        {

            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<ContratosInternosViewModel>> Listar(int pageNumber = 1, int pageSize = 5, string termo = null)
        {
            var queryParams = new List<string>
            {
                $"pageNumber={pageNumber}",
                $"pageSize={pageSize}"
    };

            if (!string.IsNullOrEmpty(termo))
            {
                queryParams.Add($"termo={termo}"); // Enviando um único termo para busca dinâmica
            }

            // Combina todos os parâmetros em uma query string
            var queryString = string.Join("&", queryParams);

            var response = await _httpClient.GetAsync($"/api/contratosinternos/listar?{queryString}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<PagedResult<ContratosInternosViewModel>>(response);
        }

        public async Task<ContratosInternosViewModel> ObterId(Guid Id)
        {
            var response = await _httpClient.GetAsync($"/api/contratosinternos/obterid/{Id}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<ContratosInternosViewModel>(response);

        }

        public async Task<ContratosInternosViewModel> Adicionar(ContratosInternosViewModel contrato)
        {
            var contratoContent = ObterConteudo(contrato);

            var response = await _httpClient.PostAsync("/api/contratosinternos/adicionar", contratoContent);

            return await DeserializarObjetoResponse<ContratosInternosViewModel>(response);

        }

        public async Task<ContratosInternosViewModel> Alterar(Guid id, ContratosInternosViewModel contrato)
        {
            var contratoContent = ObterConteudo(contrato);
            var response = await _httpClient.PostAsync($"/api/contratosinternos/alterar/{id}", contratoContent);
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<ContratosInternosViewModel>(response);
        }

        public async Task<bool> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/contratosinternos/excluir/{id}");
            return TratarErrosResponse(response);
        }


    }
}
