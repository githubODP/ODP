using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Detran;
using ODP.Web.UI.Models.ViewModels.Detran;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Detran
{
    public class VeiculoOrdemJudicialService : Service, IVeiculoOrdemJudicialService
    {
        private readonly HttpClient _httpClient;

        public VeiculoOrdemJudicialService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<VeiculoOrdemJudicialViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/veiculojudicial/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<VeiculoOrdemJudicialViewModel>>(response);
        }

        public async Task<VeiculoOrdemJudicialViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/veiculojudicial/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoOrdemJudicialViewModel>(response);
        }



        public async Task<VeiculoOrdemJudicialViewModel> BuscarPorCNPJ(string cnpj)
        {

            var response = await _httpClient.GetAsync($"api/veiculojudicial/consutacnpj/{cnpj}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoOrdemJudicialViewModel>(response);

        }


        public async Task<VeiculoOrdemJudicialViewModel> BuscarPorCPF(string cpf)

        {
            var response = await _httpClient.GetAsync($"api/veiculojudicial/consultacpf/{cpf}");
            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoOrdemJudicialViewModel>(response);
        }




    }
}
