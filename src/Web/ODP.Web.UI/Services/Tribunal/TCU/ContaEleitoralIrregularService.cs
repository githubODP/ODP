

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
    public class ContaEleitoralIrregularService : Service, IContaEleitoralIrregularService
    {
        private readonly HttpClient _httpClient;

        public ContaEleitoralIrregularService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<ContaEleitoralIrregularViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/contaeleitoralirregular/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<ContaEleitoralIrregularViewModel>>(response);
        }

        public async Task<ContaEleitoralIrregularViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/contaeleitoralirregular/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ContaEleitoralIrregularViewModel>(response);
        }

        public async Task<List<ContaEleitoralIrregularViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/contaeleitoralirregular/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<ContaEleitoralIrregularViewModel>();
                }

                var tcu = await DeserializarObjetoResponse<List<ContaEleitoralIrregularViewModel>>(response);
                return tcu;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<ContaEleitoralIrregularViewModel>();
            }
        }

        public async Task<List<ContaEleitoralIrregularViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/contaeleitoralirregular/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<ContaEleitoralIrregularViewModel>();
                }

                var tcu = await DeserializarObjetoResponse<List<ContaEleitoralIrregularViewModel>>(response);
                return tcu;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<ContaEleitoralIrregularViewModel>();
            }
        }




    }
}
