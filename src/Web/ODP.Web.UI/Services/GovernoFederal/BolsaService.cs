using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using ODP.Web.UI.Models.ViewModels.GovernoFederal;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System;

namespace ODP.Web.UI.Services.GovernoFederal
{
    public class BolsaService : Service, IBolsaService
    {
        private readonly HttpClient _httpClient;

        public BolsaService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<BolsaFamiliaViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/bolsa/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<BolsaFamiliaViewModel>>(response);
        }

        public async Task<BolsaFamiliaViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/bolsa/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<BolsaFamiliaViewModel>(response);
        }



       

        public async  Task<List<BolsaFamiliaViewModel>> BuscarCPF(string cpf)
        {           
            try
            {
                var response = await _httpClient.GetAsync($"api/bolsa/consultacnpj/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<BolsaFamiliaViewModel>();
                }

                var bolsa = await DeserializarObjetoResponse<List<BolsaFamiliaViewModel>>(response);
                return bolsa;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<BolsaFamiliaViewModel>();
            }
        }

        public Task<List<BolsaFamiliaViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
