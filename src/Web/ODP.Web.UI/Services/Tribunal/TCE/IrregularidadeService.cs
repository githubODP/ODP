

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
    public class IrregularidadeService : Service, IIrregularidadeService
    {
        private readonly HttpClient _httpClient;

        public IrregularidadeService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<IrregularidadeViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/irregularidade/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<IrregularidadeViewModel>>(response);
        }

        public async Task<IrregularidadeViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/irregularidade/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IrregularidadeViewModel>(response);
        }


        public async Task<List<IrregularidadeViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/irregularidade/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<IrregularidadeViewModel>();
                }

                var tce = await DeserializarObjetoResponse<List<IrregularidadeViewModel>>(response);
                return tce;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<IrregularidadeViewModel>();
            }
        }

        public Task<List<IrregularidadeViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
