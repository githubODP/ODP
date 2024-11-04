using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using ODP.Web.UI.Models.ViewModels.GovernoFederal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.GovernoFederal
{
    public class AeronaveService : Service, IAeronaveService
    {
        private readonly HttpClient _httpClient;

        public AeronaveService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<AeronaveViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/aeronave/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<AeronaveViewModel>>(response);
        }

        public async Task<AeronaveViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/aeronave/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<AeronaveViewModel>(response);
        }



        public async Task<List<AeronaveViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/aeronave/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<AeronaveViewModel>();
                }

                var federal = await DeserializarObjetoResponse<List<AeronaveViewModel>>(response);
                return federal;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<AeronaveViewModel>();
            }
        }

        public async Task<List<AeronaveViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/aeronave/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<AeronaveViewModel>();
                }

                var federal = await DeserializarObjetoResponse<List<AeronaveViewModel>>(response);
                return federal;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<AeronaveViewModel>();
            }
        }



    }
}
