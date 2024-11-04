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
    public class InidoneoService : Service, IInidoneoService
    {
        private readonly HttpClient _httpClient;

        public InidoneoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<InidoneoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/inidoneo/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<InidoneoViewModel>>(response);
        }

        public async Task<InidoneoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/inidoneo/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<InidoneoViewModel>(response);
        }

        public async Task<List<InidoneoViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/inidoneo/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<InidoneoViewModel>();
                }

                var tcu = await DeserializarObjetoResponse<List<InidoneoViewModel>>(response);
                return tcu;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<InidoneoViewModel>();
            }
        }

        public Task<List<InidoneoViewModel>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
