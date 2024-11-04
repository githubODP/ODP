
using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces;
using ODP.Web.UI.Models.Interfaces.Compras;
using ODP.Web.UI.Models.ViewModels.Compras;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;


namespace ODP.Web.UI.Services.Compras
{



    public class LicitacaoService : Service, ILicitacaoService
    {
        private readonly HttpClient _httpClient;

        public LicitacaoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<LicitacaoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/licitacao/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<LicitacaoViewModel>>(response);
        }

        public async Task<LicitacaoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/licitacao/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<LicitacaoViewModel>(response);
        }



        public async Task<List<LicitacaoViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/licitacao/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<LicitacaoViewModel>();
                }

                var licitacao = await DeserializarObjetoResponse<List<LicitacaoViewModel>>(response);
                return licitacao;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<LicitacaoViewModel>();
            }
        }

        

        public Task<List<LicitacaoViewModel>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
