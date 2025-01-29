using Microsoft.Extensions.Options;
using System.Net.Http;
using System;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.DueDiligence;
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
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<PagedResult<AnaliseViewModel>>(response);
        }


        public async Task<AnaliseCadastroViewModel> ObterId(Guid id)
        {
            // Substituir {id:guid} pela variável id diretamente na URL
            var response = await _httpClient.GetAsync($"api/analise/obter/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<AnaliseCadastroViewModel>(response);
        }

        public async Task<AnaliseViewModel> PesquisarComissionado(string nroProtocolo)
        {
            var response = await _httpClient.GetAsync($"/api/analise/pesquisar-comissionado/{nroProtocolo}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<AnaliseViewModel>(response);
        }

        public async Task<AnaliseCadastroViewModel> Adicionar(AnaliseCadastroViewModel analisecadastroViewModel)
        {
            var analiseContent = ObterConteudo(analisecadastroViewModel);
            var response = await _httpClient.PostAsync("/api/analise/adicionar", analiseContent);

            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<AnaliseCadastroViewModel>(response);
        }

        public async Task<AnaliseCadastroViewModel> Alterar(AnaliseCadastroViewModel analisecadastroViewModel, Guid id)
        {

            var analiseContent = ObterConteudo(analisecadastroViewModel);

            var response = await _httpClient.PutAsync("/api/analise/alterar", analiseContent);

            if (TratarErrosResponse(response))
            {
                return null;

            }
            return await DeserializarObjetoResponse<AnaliseCadastroViewModel>(response);

        }


        public async Task<AnaliseCadastroViewModel> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/analise/excluir/{id}");
            TratarErrosResponse(response);

            if (TratarErrosResponse(response))
            {
                return null;
            }
            return null;
        }

       
    }
}
