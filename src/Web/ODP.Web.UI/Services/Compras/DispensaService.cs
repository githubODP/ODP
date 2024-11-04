


using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Compras;
using ODP.Web.UI.Models.ViewModels.Compras;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
namespace ODP.Web.UI.Services.Compras
{
    public class DispensaService : Service, IDispensaService
    {
        private readonly HttpClient _httpClient;

        public DispensaService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<DispensaViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/dispensa/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DispensaViewModel>>(response);
        }


        public async Task<DispensaViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/dispensa/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DispensaViewModel>(response);
        }



        public async Task<List<DispensaViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/dispensa/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DispensaViewModel>();
                }

                var dispensa = await DeserializarObjetoResponse<List<DispensaViewModel>>(response);
                return dispensa;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DispensaViewModel>();
            }
        }

        public Task<List<DispensaViewModel>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
