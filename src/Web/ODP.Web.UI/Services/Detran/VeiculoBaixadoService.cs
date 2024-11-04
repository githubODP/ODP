using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Detran;
using ODP.Web.UI.Models.ViewModels.Detran;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Detran
{
    public class VeiculoBaixadoService : Service, IVeiculoBaixadoService
    {
        private readonly HttpClient _httpClient;

        public VeiculoBaixadoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<VeiculoBaixadoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/veiculobaixado/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<VeiculoBaixadoViewModel>>(response);
        }

        public async Task<VeiculoBaixadoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/veiculobaixado/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoBaixadoViewModel>(response);
        }



        public async Task<VeiculoBaixadoViewModel> BuscarPorCNPJ(string cnpj)
        {

            var response = await _httpClient.GetAsync($"api/veiculobaixado/consutacnpj/{cnpj}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoBaixadoViewModel>(response);

        }


        public async Task<VeiculoBaixadoViewModel> BuscarPorCPF(string cpf)

        {
            var response = await _httpClient.GetAsync($"api/veiculobaixado/consultacpf/{cpf}");
            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoBaixadoViewModel>(response);
        }




    }
}
