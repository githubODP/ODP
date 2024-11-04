
using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Detran;
using ODP.Web.UI.Models.ViewModels.Detran;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Detran
{
    public class VeiculoProntuarioService : Service, IVeiculoProntuarioService
    {
        private readonly HttpClient _httpClient;

        public VeiculoProntuarioService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<VeiculoProntuarioViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/veiculoprontuario/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<VeiculoProntuarioViewModel>>(response);
        }

        public async Task<VeiculoProntuarioViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/veiculoprontuario/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoProntuarioViewModel>(response);
        }



        public async Task<VeiculoProntuarioViewModel> BuscarPorCNPJ(string cnpj)
        {

            var response = await _httpClient.GetAsync($"api/veiculoprontuario/consutacnpj/{cnpj}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoProntuarioViewModel>(response);

        }


        public async Task<VeiculoProntuarioViewModel> BuscarPorCPF(string cpf)

        {
            var response = await _httpClient.GetAsync($"api/veiculoprontuario/consultacpf/{cpf}");
            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VeiculoProntuarioViewModel>(response);
        }




    }
}
