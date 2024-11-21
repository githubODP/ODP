using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Dividas;
using ODP.Web.UI.Models.ViewModels.Dividas;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Dividas
{
    public class DividaNaoPrevidenciaService : Service, IDividaNaoPrevidenciaService
    {
        private readonly HttpClient _httpClient;

        public DividaNaoPrevidenciaService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<DividaNaoPrevidenciariaViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/dividanaoprevidenciarias/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DividaNaoPrevidenciariaViewModel>>(response);
        }

        public async Task<DividaNaoPrevidenciariaViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/dividanaoprevidenciarias/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DividaNaoPrevidenciariaViewModel>(response);
        }



        public async Task<List<DividaNaoPrevidenciariaViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/dividanaoprevidenciarias/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DividaNaoPrevidenciariaViewModel>();
                }

                var divida = await DeserializarObjetoResponse<List<DividaNaoPrevidenciariaViewModel>>(response);
                return divida;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DividaNaoPrevidenciariaViewModel>();
            }
        }

        public async Task<List<DividaNaoPrevidenciariaViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/dividanaoprevidenciarias/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DividaNaoPrevidenciariaViewModel>();
                }

                var divida = await DeserializarObjetoResponse<List<DividaNaoPrevidenciariaViewModel>>(response);
                return divida;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DividaNaoPrevidenciariaViewModel>();
            }
        }



    }
}
