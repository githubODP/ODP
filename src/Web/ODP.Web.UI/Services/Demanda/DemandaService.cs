using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Demandas;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Demanda
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


        public async Task<PagedResult<DemandaViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/demanda/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DemandaViewModel>>(response);
        }



        public async Task<DemandaViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/demanda/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DemandaViewModel>(response);
        }

        public async Task<DemandaViewModel> Adicionar(DemandaViewModel dueViewModel)
        {
            var dueContent = ObterConteudo(dueViewModel);

            var response = await _httpClient.PostAsync("/api/demanda/adicionar", dueContent);

            if (TratarErrosResponse(response))
            {
                return null;

            }
            return await DeserializarObjetoResponse<DemandaViewModel>(response);

        }

        public async Task<DemandaViewModel> Alterar(DemandaViewModel dueViewModel, Guid id)
        {

            var dueContent = ObterConteudo(dueViewModel);

            var response = await _httpClient.PutAsync("/api/demanda/alterar", dueContent);

            if (TratarErrosResponse(response))
            {
                return null;

            }
            return await DeserializarObjetoResponse<DemandaViewModel>(response);

        }



        public async Task<DemandaViewModel> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/demanda/deletar/{id}");
            TratarErrosResponse(response);

            if (TratarErrosResponse(response))
            {
                return null;
            }
            return null;
        }


        public async Task<List<DemandaViewModel>> BuscarCNPJ(string cnpj)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/demanda/consultacpf/{cnpj}");
                TratarErrosResponse(response);

                // Verificar se a resposta contém conteúdo
                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DemandaViewModel>(); // Retorna um modelo vazio
                }

                var demanda = await DeserializarObjetoResponse<List<DemandaViewModel>>(response);
                return demanda;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Retornar um modelo vazio se a demanda não for encontrado (404)
                return new List<DemandaViewModel>();
            }
        }
        public async Task<List<DemandaViewModel>> BuscarCPF(string cpf)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/demanda/consultacpf/{cpf}");
                TratarErrosResponse(response);

                // Verificar se a resposta contém conteúdo
                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DemandaViewModel>(); // Retorna um modelo vazio
                }

                var demanda = await DeserializarObjetoResponse<List<DemandaViewModel>>(response);
                return demanda;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Retornar um modelo vazio se a demanda não for encontrado (404)
                return new List<DemandaViewModel>();
            }
        }
    }

}

