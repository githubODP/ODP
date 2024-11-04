using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Detran;
using ODP.Web.UI.Models.ViewModels.Detran;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Detran
{
    public class VeiculoBloqueadoService : Service, IVeiculoBloqueadoService
    {
        private readonly HttpClient _httpClient;

        public VeiculoBloqueadoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<VeiculoBloqueioRouboViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/veiculoroubo/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<VeiculoBloqueioRouboViewModel>>(response);
        }

        public async Task<VeiculoBloqueioRouboViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/veiculoroubo/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoBloqueioRouboViewModel>(response);
        }



        public async Task<VeiculoBloqueioRouboViewModel> BuscarPorCNPJ(string cnpj)
        {

            var response = await _httpClient.GetAsync($"api/veiculoroubo/consutacnpj/{cnpj}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoBloqueioRouboViewModel>(response);

        }


        public async Task<VeiculoBloqueioRouboViewModel> BuscarPorCPF(string cpf)

        {
            var response = await _httpClient.GetAsync($"api/veiculoroubo/consultacpf/{cpf}");
            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoBloqueioRouboViewModel>(response);
        }




    }
}
