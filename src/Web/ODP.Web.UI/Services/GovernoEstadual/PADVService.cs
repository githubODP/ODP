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
    public class PADVService : Service, IPADVService
    {
        private readonly HttpClient _httpClient;

        public PADVService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<PADvViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/padv/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<PADvViewModel>>(response);
        }

        public async Task<PADvViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/padv/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PADvViewModel>(response);
        }



        public async Task<List<PADvViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/padv/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<PADvViewModel>();
                }

                var estadual = await DeserializarObjetoResponse<List<PADvViewModel>>(response);
                return estadual;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<PADvViewModel>();
            }
        }

        public Task<List<PADvViewModel>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
