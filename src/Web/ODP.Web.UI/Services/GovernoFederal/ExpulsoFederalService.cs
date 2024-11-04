using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using ODP.Web.UI.Models.ViewModels.GovernoFederal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.GovernoFederal
{
    public class ExpulsoFederalService : Service, IExpulsoFederalService
    {
        private readonly HttpClient _httpClient;

        public ExpulsoFederalService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<ExpulsoFederalViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/expulsofederal/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<ExpulsoFederalViewModel>>(response);
        }

        public async Task<ExpulsoFederalViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/expulsofederal/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ExpulsoFederalViewModel>(response);
        }






        public async Task<List<ExpulsoFederalViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/expulsofederal/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<ExpulsoFederalViewModel>();
                }

                var federal = await DeserializarObjetoResponse<List<ExpulsoFederalViewModel>>(response);
                return federal;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<ExpulsoFederalViewModel>();
            }
        }

        public Task<List<ExpulsoFederalViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
