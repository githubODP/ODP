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
    public class NFEletronicaFederalService : Service, INFEletronicaFederalService
    {
        private readonly HttpClient _httpClient;

        public NFEletronicaFederalService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<NFEletronicaFederalViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/notasfiscaisfederais/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<NFEletronicaFederalViewModel>>(response);
        }

        public async Task<NFEletronicaFederalViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/notasfiscaisfederais/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<NFEletronicaFederalViewModel>(response);
        }



        public async Task<List<NFEletronicaFederalViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/notasfiscaisfederais/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<NFEletronicaFederalViewModel>();
                }

                var fazenda = await DeserializarObjetoResponse<List<NFEletronicaFederalViewModel>>(response);
                return fazenda;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<NFEletronicaFederalViewModel>();
            }
        }

        public async Task<List<NFEletronicaFederalViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/notasfiscaisfederais/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<NFEletronicaFederalViewModel>();
                }

                var fazenda = await DeserializarObjetoResponse<List<NFEletronicaFederalViewModel>>(response);
                return fazenda;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<NFEletronicaFederalViewModel>();
            }
        }
    }
}
