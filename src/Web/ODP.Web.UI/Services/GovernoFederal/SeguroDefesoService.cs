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
    public class SeguroDefesoService : Service, ISeguroDefesoService
    {
        private readonly HttpClient _httpClient;

        public SeguroDefesoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }



        public async Task<PagedResult<SeguroDefesoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/segurodefeso/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<SeguroDefesoViewModel>>(response);
        }

        public async Task<SeguroDefesoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/segurodefeso/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<SeguroDefesoViewModel>(response);
        }



        public async Task<List<SeguroDefesoViewModel>> BuscarCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/segurodefeso/consultacpf/{cpf}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<SeguroDefesoViewModel>();
                }

                var federal = await DeserializarObjetoResponse<List<SeguroDefesoViewModel>>(response);
                return federal;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<SeguroDefesoViewModel>();
            }
        }

        public Task<List<SeguroDefesoViewModel>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }
    }
}
