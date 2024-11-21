

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
    public class JuceparService : Service, IJuceparService
    {
        private readonly HttpClient _httpClient;

        public JuceparService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<JuceparViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/jucepar/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<JuceparViewModel>>(response);
        }

        public async Task<JuceparViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/jucepar/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<JuceparViewModel>(response);
        }



        public async Task<List<JuceparViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/jucepar/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<JuceparViewModel>();
                }

                var fazenda = await DeserializarObjetoResponse<List<JuceparViewModel>>(response);
                return fazenda;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<JuceparViewModel>();
            }
        }

        public async Task<List<JuceparViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/jucepar/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<JuceparViewModel>();
                }

                var fazenda = await DeserializarObjetoResponse<List<JuceparViewModel>>(response);
                return fazenda;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<JuceparViewModel>();
            }
        }




    }
}
