﻿using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Interfaces.Tribunal.TSE;
using ODP.Web.UI.Models.ViewModels.Tribunal.TSE;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ODP.Web.UI.Services.Tribunal.TSE
{
    public class DoacaoPartidoService : Service, IDoacaoPartidoService
    {
        private readonly HttpClient _httpClient;

        public DoacaoPartidoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }
        public async Task<PagedResult<DoacaoPartidoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"api/doacaopartido/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DoacaoPartidoViewModel>>(response);
        }

        public async Task<DoacaoPartidoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/doacaopartido/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DoacaoPartidoViewModel>(response);
        }

        public async Task<List<DoacaoPartidoViewModel>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlEncode(cnpj);
            try
            {
                var response = await _httpClient.GetAsync($"api/doacaopartido/consultacnpj/{cnpj}");
                TratarErrosResponse(response);


                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DoacaoPartidoViewModel>();
                }

                var tse = await DeserializarObjetoResponse<List<DoacaoPartidoViewModel>>(response);
                return tse;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DoacaoPartidoViewModel>();
            }
        }

        public async Task<List<DoacaoPartidoViewModel>> BuscarCPF(string cpf)
        {
            cpf = HttpUtility.UrlEncode(cpf);
            try
            {
                var response = await _httpClient.GetAsync($"api/doacaopartido/consultacpf/{cpf}");
                TratarErrosResponse(response);

                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DoacaoPartidoViewModel>();
                }

                var tse = await DeserializarObjetoResponse<List<DoacaoPartidoViewModel>>(response);
                return tse;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {

                return new List<DoacaoPartidoViewModel>();
            }
        }
    }
}
