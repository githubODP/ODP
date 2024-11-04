using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Detran;
using ODP.Web.UI.Models.ViewModels.Detran;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Detran
{
    public class VeiculoCirculacaoService : Service, IVeiculoCirculacaoService
    {
        private readonly HttpClient _httpClient;

        public VeiculoCirculacaoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<VeiculoCirculacaoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/veiculocirculacao/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<VeiculoCirculacaoViewModel>>(response);
        }

        public async Task<VeiculoCirculacaoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/veiculocirculacao/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoCirculacaoViewModel>(response);
        }



        public async Task<VeiculoCirculacaoViewModel> BuscarPorCNPJ(string cnpj)
        {

            var response = await _httpClient.GetAsync($"api/veiculocirculacao/consutacnpj/{cnpj}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoCirculacaoViewModel>(response);

        }


        public async Task<VeiculoCirculacaoViewModel> BuscarPorCPF(string cpf)

        {
            var response = await _httpClient.GetAsync($"api/veiculocirculacao/consultacpf/{cpf}");
            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoCirculacaoViewModel>(response);
        }




    }
}
