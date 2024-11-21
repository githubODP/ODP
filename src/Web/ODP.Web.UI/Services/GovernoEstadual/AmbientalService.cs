using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.GovernoEstadual;
using ODP.Web.UI.Models.ViewModels.GovernoEstadual;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.GovernoEstadual
{
    public class AmbientalService : Service, IAmbientalService
    {
        private readonly HttpClient _httpClient;

        public AmbientalService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<AmbientalViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/ambiental/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<AmbientalViewModel>>(response);
        }

        public async Task<AmbientalViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/ambiental/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<AmbientalViewModel>(response);
        }



        public async Task<List<AmbientalViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/ambiental/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<AmbientalViewModel>();
                }

                var estadual = await DeserializarObjetoResponse<List<AmbientalViewModel>>(response);
                return estadual;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<AmbientalViewModel>();
            }
        }

        public async Task<List<AmbientalViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/ambiental/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<AmbientalViewModel>();
                }

                var estadual = await DeserializarObjetoResponse<List<AmbientalViewModel>>(response);
                return estadual;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<AmbientalViewModel>();
            }
        }



    }
}
