using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.RecursosHumanos;
using ODP.Web.UI.Models.ViewModels.RecursosHumanos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.RecursosHumanos
{
    public class DependenteService : Service, IDependenteService
    {
        private readonly HttpClient _httpClient;

        public DependenteService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);


            _httpClient = httpClient;

        }



        public async Task<PagedResult<DependenteViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/recursoshumanos/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DependenteViewModel>>(response);
        }

        public async Task<DependenteViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/recursoshumanos/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DependenteViewModel>(response);
        }




        public async Task<List<DependenteViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/recursoshumanos/consultacpf/{cpf}");
                TratarErrosResponse(response);

                // Verificar se a resposta contém conteúdo
                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DependenteViewModel>(); // Retorna um modelo vazio
                }

                var dependente = await DeserializarObjetoResponse<List<DependenteViewModel>>(response);
                return dependente;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Retornar um modelo vazio se o funcionario não for encontrado (404)
                return new List<DependenteViewModel>();
            }
        }

        public Task<List<DependenteViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
