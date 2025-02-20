using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Cooperacao;
using ODP.Web.UI.Models.Internos;
using System;
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



        public async Task<ContratosInternosViewModel> Adicionar(ContratosInternosViewModel contratos)
        {
            var content = ObterConteudo(contratos);

            var response = await _httpClient.PostAsync("/api/contratrosinternos/adicionar", content);

            return await DeserializarObjetoResponse<ContratosInternosViewModel>(response);
        }

        public async Task<ContratosInternosViewModel> Alterar(ContratosInternosViewModel contratos)
        {
            var content = ObterConteudo(contratos);

            var response = await _httpClient.PostAsync("/api/contratrosinternos/alterar", content);

            return await DeserializarObjetoResponse<ContratosInternosViewModel>(response);
        }

        public async Task<ContratosInternosViewModel> Deletar(ContratosInternosViewModel contratos)
        {
            var content = ObterConteudo(contratos);

            var response = await _httpClient.PostAsync("/api/contratrosinternos/excluir", content);

            return await DeserializarObjetoResponse<ContratosInternosViewModel>(response);
        }

        public async Task<PagedResult<ContratosInternosViewModel>> Listar(int pageNumber = 1, int pageSize = 5)
        {
            var response = await _httpClient.GetAsync($"api/contratrosinternos/listar?pageNumber={pageNumber}&pageSize={pageSize}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<PagedResult<ContratosInternosViewModel>>(response);
        }

        public async Task<ContratosInternosViewModel> ObterId(Guid Id)
        {
            var response = await _httpClient.GetAsync($"api/contratrosinternos/obterid/{Id}");

            var teste = DeserializarObjetoResponse<ContratosInternosViewModel>(response);
            return await teste;
        }
    }
}
