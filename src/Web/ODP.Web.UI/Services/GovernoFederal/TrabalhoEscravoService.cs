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
    public class TrabalhoEscravoService : Service, ITrabalhoEscravoService
    {
        private readonly HttpClient _httpClient;

        public TrabalhoEscravoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<TrabalhoEscravoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/trabalhoescravo/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<TrabalhoEscravoViewModel>>(response);
        }

        public async Task<TrabalhoEscravoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/trabalhoescravo/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<TrabalhoEscravoViewModel>(response);
        }



        public async Task<List<TrabalhoEscravoViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/trabalhoescravo/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<TrabalhoEscravoViewModel>();
                }

                var federal = await DeserializarObjetoResponse<List<TrabalhoEscravoViewModel>>(response);
                return federal;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<TrabalhoEscravoViewModel>();
            }
        }

        public async Task<List<TrabalhoEscravoViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/trabalhoescravo/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<TrabalhoEscravoViewModel>();
                }

                var federal = await DeserializarObjetoResponse<List<TrabalhoEscravoViewModel>>(response);
                return federal;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<TrabalhoEscravoViewModel>();
            }
        }




    }
}
