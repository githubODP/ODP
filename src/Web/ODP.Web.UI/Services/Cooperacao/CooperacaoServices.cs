using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Cooperacao;
using ODP.Web.UI.Models.Corregedoria;
using ODP.Web.UI.Models.DueDiligence;
using ODP.Web.UI.Services.Corregedoria;
using PdfSharpCore;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Cooperacao
{
    public class CooperacaoServices : Service, IcooperacaoServices
    {
        private readonly HttpClient _httpClient;

        public CooperacaoServices( HttpClient httpClient,
           IOptions<AppSettings> settings)
        {
           
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<TermoCooperacaoViewModel> Adicionar(TermoCooperacaoViewModel termo)
        {
            var dueContent = ObterConteudo(termo);

            var response = await _httpClient.PostAsync("/api/termocooperacao/adicionar", dueContent);

             return await DeserializarObjetoResponse<TermoCooperacaoViewModel>(response);

        }

        public async Task<TermoCooperacaoViewModel> Alterar(TermoCooperacaoViewModel termo)
        {
            var dueContent = ObterConteudo(termo);

            var response = await _httpClient.PostAsync("/api/termocooperacao/alterar", dueContent);

            if (TratarErrosResponse(response))
            {
                return await DeserializarObjetoResponse<TermoCooperacaoViewModel>(response);

            }
            return null;


        }

        public async Task<TermoCooperacaoViewModel> Deletar(TermoCooperacaoViewModel termo)
        {
            var dueContent = ObterConteudo(termo);

            var response = await _httpClient.PostAsync("/api/termocooperacao/excluir", dueContent);

            if (TratarErrosResponse(response))
            {
                return await DeserializarObjetoResponse<TermoCooperacaoViewModel>(response);

            }
            return null;
        }

        public async Task<PagedResult<TermoCooperacaoViewModel>> ListarComFiltros(int pageNumber = 1, int pageSize = 5)
        {
            var response = await _httpClient.GetAsync($"api/termocooperacao/listar?pageNumber={pageNumber}&pageSize={pageSize}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<PagedResult<TermoCooperacaoViewModel>>(response);
        }

        public async Task<TermoCooperacaoViewModel> ObterProtocolo(string protocolo)
        {
            var response = await _httpClient.GetAsync($"api/termocooperacao/pesquisar/{protocolo}");
            TratarErrosResponse(response);
            var teste = DeserializarObjetoResponse<TermoCooperacaoViewModel>(response);
            return await teste;
        }
    }
}
