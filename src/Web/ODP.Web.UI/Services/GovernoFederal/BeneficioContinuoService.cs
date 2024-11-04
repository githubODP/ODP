using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using ODP.Web.UI.Models.ViewModels.GovernoFederal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.GovernoFederal
{
    public class BeneficioContinuoService : Service, IBeneficioContinuoService
    {
        private readonly HttpClient _httpClient;

        public BeneficioContinuoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<BeneficioContinuoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/beneficio/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<BeneficioContinuoViewModel>>(response);
        }

        public async Task<BeneficioContinuoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/beneficio/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<BeneficioContinuoViewModel>(response);
        }





        public async Task<List<BeneficioContinuoViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/beneficio/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<BeneficioContinuoViewModel>();
                }

                var federal = await DeserializarObjetoResponse< List<BeneficioContinuoViewModel>>(response);
                return federal;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<BeneficioContinuoViewModel>();
            }
        }

        public Task<List<BeneficioContinuoViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
