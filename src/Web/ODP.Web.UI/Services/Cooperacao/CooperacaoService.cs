using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Cooperacao;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Cooperacao
{
    public class CooperacaoService : Service, ICooperacaoService
    {
        private readonly HttpClient _httpClient;

        public CooperacaoService(HttpClient httpClient,
           IOptions<AppSettings> settings)
        {

            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }

        public async Task<PagedResult<TermoCooperacaoViewModel>> Listar(int pageNumber = 1, int pageSize = 5, string termo = null)
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

            var response = await _httpClient.GetAsync($"/api/termocooperacao/listar?{queryString}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<PagedResult<TermoCooperacaoViewModel>>(response);
        }

        

        public async Task<TermoCooperacaoViewModel> ObterId(Guid Id)
        {
            var response = await _httpClient.GetAsync($"/api/termocooperacao/obterid/{Id}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<TermoCooperacaoViewModel>(response);
            
        }


        public async Task<TermoCooperacaoViewModel> Adicionar(TermoCooperacaoViewModel termo)
        {
            var cooperacaoContent = ObterConteudo(termo);

            var response = await _httpClient.PostAsync("/api/termocooperacao/adicionar", cooperacaoContent);

            return await DeserializarObjetoResponse<TermoCooperacaoViewModel>(response);

        }

        public async Task<TermoCooperacaoViewModel> Alterar(Guid id, TermoCooperacaoViewModel termo)
        {
            var cooperacaoContent = ObterConteudo(termo);
            var response = await _httpClient.PostAsync($"/api/termocooperacao/alterar/{id}", cooperacaoContent);
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<TermoCooperacaoViewModel>(response);
        }

        public async Task<bool> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/termocooperacao/excluir/{id}");
            return TratarErrosResponse(response);
        }



        public async Task<List<TermoCooperacaoViewModel>> VerificarAlertasFimVigencia()
        {
            var response = await _httpClient.GetAsync("/api/termocooperacao/listar-envio");

            if (!response.IsSuccessStatusCode)
            {
                return new List<TermoCooperacaoViewModel>();
            }
            TratarErrosResponse(response);
            return await  DeserializarObjetoResponse<List<TermoCooperacaoViewModel>>(response);

            
        }
    }
}
