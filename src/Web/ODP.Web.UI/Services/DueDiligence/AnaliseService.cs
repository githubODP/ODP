using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.DueDiligence;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.DueDiligence
{
    public class AnaliseService : Service, IAnaliseService
    {
        private readonly HttpClient _httpClient;

        public AnaliseService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }




        public async Task<PagedResult<AnaliseViewModel>> ListarDadosAdicionais(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/analise/listaradicionais?pageNumber={pageNumber}&pageSize={pageSize}");

            Console.WriteLine($"Status da resposta: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Erro na API: {response.StatusCode}");
                return null; // Evita que a exception suba
            }

            return await DeserializarObjetoResponse<PagedResult<AnaliseViewModel>>(response);
        }


        public async Task<AnaliseCadastroViewModel> ObterId(Guid id)
        {

            var response = await _httpClient.GetAsync($"api/analise/obter/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<AnaliseCadastroViewModel>(response);
        }



        public async Task<List<DueDiligenceViewModel>> PesquisarComissionado(string nroProtocolo)
        {
            var response = await _httpClient.GetAsync($"/api/analise/pesquisar-comissionado/{nroProtocolo}");
            TratarErrosResponse(response);

            // Lê o JSON completo e extrai apenas o campo "dados"
            var json = await response.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<JsonResponse<List<DueDiligenceViewModel>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return resultado?.dados ?? new List<DueDiligenceViewModel>();
        }

        // Criar uma classe auxiliar para mapear o JSON retornado
        private class JsonResponse<T>
        {
            public bool sucesso { get; set; }
            public T dados { get; set; }
        }


        public async Task<AnaliseCadastroViewModel> Adicionar(AnaliseCadastroViewModel analiseCadastroViewModel)
        {
            var analiseContent = ObterConteudo(analiseCadastroViewModel);

            var response = await _httpClient.PostAsync("/api/analise/adicionar", analiseContent);

            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<AnaliseCadastroViewModel>(response);
        }

        public async Task<AnaliseCadastroViewModel> Alterar(Guid id, AnaliseCadastroViewModel analiseCadastroViewModel)
        {
            var analiseContent = ObterConteudo(analiseCadastroViewModel);
            var response = await _httpClient.PostAsync($"/api/analise/alterar/{id}", analiseContent);
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<AnaliseCadastroViewModel>(response);
        }

        public async Task<bool> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/analise/excluir/{id}");
            return TratarErrosResponse(response);
        }



    }
}
