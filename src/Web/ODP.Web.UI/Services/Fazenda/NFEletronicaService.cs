using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Fazenda;
using ODP.Web.UI.Models.ViewModels.Fazenda;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Fazenda
{
    public class NFEletronicaService : Service, INFEletronicaService
    {
        private readonly HttpClient _httpClient;

        public NFEletronicaService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<NFEletronicaViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/notasfiscais/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<NFEletronicaViewModel>>(response);
        }

        public async Task<NFEletronicaViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/notasfiscais/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<NFEletronicaViewModel>(response);
        }



        public async Task<List<NFEletronicaViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/notasfiscais/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<NFEletronicaViewModel>();
                }

                var fazenda = await DeserializarObjetoResponse< List<NFEletronicaViewModel>>(response);
                return fazenda;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<NFEletronicaViewModel>();
            }
        }

        public Task<List<NFEletronicaViewModel>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }

}
