using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCU;
using ODP.Web.UI.Models.ViewModels.Tribunal.TCU;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Tribunal.TCU
{
    public class InabilitadoService : Service, IInabilitadoService
    {
        private readonly HttpClient _httpClient;

        public InabilitadoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<InabilitadoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/inabilitado/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<InabilitadoViewModel>>(response);
        }

        public async Task<InabilitadoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/inabilitado/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<InabilitadoViewModel>(response);
        }


        public async Task<List<InabilitadoViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/inabilitado/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<InabilitadoViewModel>();
                }

                var tcu = await DeserializarObjetoResponse<List<InabilitadoViewModel>>(response);
                return tcu;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<InabilitadoViewModel>();
            }
        }

        public Task<List<InabilitadoViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
