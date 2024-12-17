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
    public class FuncionarioService : Service, IFuncionarioService
    {
        private readonly HttpClient _httpClient;

        public FuncionarioService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);


            _httpClient = httpClient;

        }



        public async Task<PagedResult<FuncionarioViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/recursoshumanos/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<FuncionarioViewModel>>(response);
        }

        public async Task<FuncionarioViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/recursoshumanos/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<FuncionarioViewModel>(response);
        }




        public async Task<List<FuncionarioViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/recursoshumanos/consultacpf/{cpf}");
                TratarErrosResponse(response);

                // Verificar se a resposta contém conteúdo
                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<FuncionarioViewModel>(); // Retorna um modelo vazio
                }

                var funcionario = await DeserializarObjetoResponse<List<FuncionarioViewModel>>(response);
                return funcionario;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Retornar um modelo vazio se o funcionario não for encontrado (404)
                return new List<FuncionarioViewModel>();
            }
        }

        public Task<List<FuncionarioViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
