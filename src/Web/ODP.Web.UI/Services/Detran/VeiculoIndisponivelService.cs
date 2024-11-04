using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Detran;
using ODP.Web.UI.Models.ViewModels.Detran;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Detran
{
    public class VeiculoIndisponivelService : Service, IVeiculoIndisponivelService
    {
        private readonly HttpClient _httpClient;

        public VeiculoIndisponivelService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<VeiculoIndispAdminViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/veiculoindisAdmin/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<VeiculoIndispAdminViewModel>>(response);
        }

        public async Task<VeiculoIndispAdminViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/veiculoindisAdmin/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoIndispAdminViewModel>(response);
        }



        public async Task<VeiculoIndispAdminViewModel> BuscarPorCNPJ(string cnpj)
        {

            var response = await _httpClient.GetAsync($"api/veiculoindisAdmin/consutacnpj/{cnpj}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoIndispAdminViewModel>(response);

        }


        public async Task<VeiculoIndispAdminViewModel> BuscarPorCPF(string cpf)

        {
            var response = await _httpClient.GetAsync($"api/veiculoindisAdmin/consultacpf/{cpf}");
            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoIndispAdminViewModel>(response);
        }




    }
}
