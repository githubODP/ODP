using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Tribunal.TSE;
using ODP.Web.UI.Models.ViewModels.Tribunal.TSE;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Tribunal.TSE
{
    public class DoacaoGeralService : Service, IDoacaoGeralService
    {
        private readonly HttpClient _httpClient;

        public DoacaoGeralService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<DoacaoGeralViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/doacaogeral/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DoacaoGeralViewModel>>(response);
        }

        public async Task<DoacaoGeralViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/doacaogeral/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DoacaoGeralViewModel>(response);
        }


        public async Task<List<DoacaoGeralViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/doacaogeral/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DoacaoGeralViewModel>();
                }

                var tse = await DeserializarObjetoResponse<List<DoacaoGeralViewModel>>(response);
                return tse;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DoacaoGeralViewModel>();
            }
        }

        public Task<List<DoacaoGeralViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
