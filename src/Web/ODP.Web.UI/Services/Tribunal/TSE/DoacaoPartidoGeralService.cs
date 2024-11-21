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
    public class DoacaoPartidoGeralService : Service, IDoacaoPartidoGeralService
    {
        private readonly HttpClient _httpClient;

        public DoacaoPartidoGeralService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<DoacaoPartidoGeralViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/doacaopartidogeral/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DoacaoPartidoGeralViewModel>>(response);
        }

        public async Task<DoacaoPartidoGeralViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/doacaopartidogeral/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DoacaoPartidoGeralViewModel>(response);
        }



        public async Task<List<DoacaoPartidoGeralViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/doacaopartidogeral/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DoacaoPartidoGeralViewModel>();
                }

                var tse = await DeserializarObjetoResponse<List<DoacaoPartidoGeralViewModel>>(response);
                return tse;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DoacaoPartidoGeralViewModel>();
            }
        }

        public Task<List<DoacaoPartidoGeralViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
