

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
    public class AcordoLenienciaService : Service, IAcordoLenienciaService
    {
        private readonly HttpClient _httpClient;

        public AcordoLenienciaService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<AcordoLenienciaViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/acordo/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<AcordoLenienciaViewModel>>(response);
        }

        public async Task<AcordoLenienciaViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/acordo/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<AcordoLenienciaViewModel>(response);
        }



        public async Task<List<AcordoLenienciaViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/acordo/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<AcordoLenienciaViewModel>();
                }

                var federal = await DeserializarObjetoResponse<List<AcordoLenienciaViewModel>>(response);
                return federal;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<AcordoLenienciaViewModel>();
            }
        }

        public Task<List<AcordoLenienciaViewModel>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
