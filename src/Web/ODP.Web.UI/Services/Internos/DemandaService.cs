using Domain.Compras.Entidades;
using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Internos;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Internos
{
    public class DemandaService : Service, IDemandaService
    {
        private readonly HttpClient _httpClient;

        public DemandaService(HttpClient httpClient,
          IOptions<AppSettings> settings)
        {

            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }

        public async Task<DemandasViewModel> Adicionar(DemandasViewModel demandas)
        {
            var content = ObterConteudo(demandas);

            var response = await _httpClient.PostAsync("/api/demanda/adicionar", content);

            return await DeserializarObjetoResponse<DemandasViewModel>(response);
        }

        public async Task<DemandasViewModel> Alterar(DemandasViewModel demandas)
        {
            var content = ObterConteudo(demandas);

            var response = await _httpClient.PostAsync("/api/demanda/alterar", content);

            return await DeserializarObjetoResponse<DemandasViewModel>(response);
        }

        public Task<DemandasViewModel> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

        public Task<DemandasViewModel> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public async Task<DemandasViewModel> Deletar(DemandasViewModel demandas)
        {

            var id = demandas.Id;
            var response = await _httpClient.GetAsync("/api/demanda/deletar/{id}");

            return await DeserializarObjetoResponse<DemandasViewModel>(response); ;
        }

        public async Task<PagedResult<DemandasViewModel>> Listar(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"api/demanda/listar?pageNumber={pageNumber}&pageSize={pageSize}");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<PagedResult<DemandasViewModel>>(response);
        }

        public async Task<DemandasViewModel> ObterId(Guid Id)
        {
            var response = await _httpClient.GetAsync($"api/demanda/buscaId/{Id}");
            var teste = DeserializarObjetoResponse<DemandasViewModel>(response);
            return await teste;
        }
    }
}
