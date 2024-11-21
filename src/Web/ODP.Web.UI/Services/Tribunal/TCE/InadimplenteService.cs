using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCE;
using ODP.Web.UI.Models.ViewModels.Tribunal.TCE;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Tribunal.TCE
{
    public class InadimplenteService : Service, IInadimplenteService
    {
        private readonly HttpClient _httpClient;

        public InadimplenteService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<InadimplenteViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/inadimplente/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<InadimplenteViewModel>>(response);
        }

        public async Task<InadimplenteViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/inadimplente/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<InadimplenteViewModel>(response);
        }



        public async Task<List<InadimplenteViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/inadimplente/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<InadimplenteViewModel>();
                }

                var tce = await DeserializarObjetoResponse<List<InadimplenteViewModel>>(response);
                return tce;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<InadimplenteViewModel>();
            }
        }

        public async Task<List<InadimplenteViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/inadimplente/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<InadimplenteViewModel>();
                }

                var tce = await DeserializarObjetoResponse<List<InadimplenteViewModel>>(response);
                return tce;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<InadimplenteViewModel>();
            }
        }


    }
}
