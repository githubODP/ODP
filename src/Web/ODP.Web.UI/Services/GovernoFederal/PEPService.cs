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
    public class PEPService : Service, IPEPService
    {
        private readonly HttpClient _httpClient;

        public PEPService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<PEPViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/pep/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<PEPViewModel>>(response);
        }

        public async Task<PEPViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/pep/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PEPViewModel>(response);
        }





        public async Task<List<PEPViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/pep/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<PEPViewModel>();
                }

                var federal = await DeserializarObjetoResponse<List<PEPViewModel>>(response);
                return federal;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<PEPViewModel>();
            }
        }

        public Task<List<PEPViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
