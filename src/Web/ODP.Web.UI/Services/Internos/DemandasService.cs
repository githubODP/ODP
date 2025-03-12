using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Internos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Internos
{
    public class DemandasService : Service, IDemandasService
    {
        private readonly HttpClient _httpClient;

        public DemandasService(HttpClient httpClient,

            IOptions<AppSettings> settings)
        {



            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }


        public async Task<PagedResult<DemandasViewModel>> Listar(int pageNumber = 1, int pageSize = 5, string termo = null)
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

            var response = await _httpClient.GetAsync($"/api/demanda/listar?{queryString}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<PagedResult<DemandasViewModel>>(response);
        }



        public async Task<DemandasViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/demanda/obterid/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DemandasViewModel>(response);
        }

        public async Task<DemandasViewModel> Adicionar(DemandasViewModel demandaViewModel)
        {
            var demandaContent = ObterConteudo(demandaViewModel);

            var response = await _httpClient.PostAsync("/api/demanda/adicionar", demandaContent);

            return await DeserializarObjetoResponse<DemandasViewModel>(response);

        }

        public async Task<DemandasViewModel> Alterar(Guid id, DemandasViewModel demandaViewModel)
        {
            var demandaContent = ObterConteudo(demandaViewModel);
            var response = await _httpClient.PostAsync($"/api/demanda/alterar/{id}", demandaContent);
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<DemandasViewModel>(response);
        }

        public async Task<bool> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/demanda/excluir/{id}");
            return TratarErrosResponse(response);
        }



        public async Task<List<DemandasViewModel>> BuscarCNPJ(string cnpj)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/demanda/consultacpf/{cnpj}");
                TratarErrosResponse(response);

                // Verificar se a resposta contém conteúdo
                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DemandasViewModel>(); // Retorna um modelo vazio
                }

                var demanda = await DeserializarObjetoResponse<List<DemandasViewModel>>(response);
                return demanda;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Retornar um modelo vazio se a demanda não for encontrado (404)
                return new List<DemandasViewModel>();
            }
        }
        public async Task<List<DemandasViewModel>> BuscarCPF(string cpf)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/demanda/consultacpf/{cpf}");
                TratarErrosResponse(response);

                // Verificar se a resposta contém conteúdo
                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DemandasViewModel>(); // Retorna um modelo vazio
                }

                var demanda = await DeserializarObjetoResponse<List<DemandasViewModel>>(response);
                return demanda;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Retornar um modelo vazio se a demanda não for encontrado (404)
                return new List<DemandasViewModel>();
            }
        }
    }
}

