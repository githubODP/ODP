﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Corregedoria;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;


namespace ODP.Web.UI.Services.Corregedoria
{
    public class InstauracaoService : Service, IInstauracaoService
    {
        private readonly HttpClient _httpClient;

        public InstauracaoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }

        public async Task<PagedResult<InstauracaoViewModel>> Listar(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync($"/api/corregedoria/listar?pageNumber={pageNumber}&pageSize={pageSize}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<InstauracaoViewModel>>(response);
        }


        public async Task<InstauracaoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/corregedoria/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<InstauracaoViewModel>(response);
        }

        public async Task<InstauracaoViewModel> Adicionar(InstauracaoViewModel instauracaoViewModel)
        {
            var dueContent = ObterConteudo(instauracaoViewModel);

            var response = await _httpClient.PostAsync("/api/corregedoria/adicionar", dueContent);

            if (TratarErrosResponse(response))
            {
                return null;

            }
            return await DeserializarObjetoResponse<InstauracaoViewModel>(response);

        }

        public async Task<InstauracaoViewModel> Alterar(InstauracaoViewModel instauracaoViewModel, Guid id)
        {

            var instauracaoContent = ObterConteudo(instauracaoViewModel);

            var response = await _httpClient.PutAsync("/api/corregedoria/alterar", instauracaoContent);

            if (TratarErrosResponse(response))
            {
                return null;
            }
            return await DeserializarObjetoResponse<InstauracaoViewModel>(response);


        }
        public async Task<InstauracaoViewModel> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/corregedoria/deletar/{id}");
            TratarErrosResponse(response);

            if (TratarErrosResponse(response))
            {
                return null;
            }
            return null;
        }


        public async Task<FileStreamResult> GerarPdf(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/corregedoria/gerarpdf/{id}");



            if (response.IsSuccessStatusCode)
            {

                var pdfContent = await response.Content.ReadAsByteArrayAsync();


                var memoryStream = new MemoryStream(pdfContent);


                return new FileStreamResult(memoryStream, "application/pdf")
                {
                    FileDownloadName = "elemento.pdf"
                };
            }
            else
            {

                return null;
            }
        }


        public async Task<IEnumerable<InstauracaoDTO>> UploadCsv(IFormFile file, int pageSize, int pageIndex)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo CSV inválido.", nameof(file));

            using (var content = new MultipartFormDataContent())
            {
                var streamContent = new StreamContent(file.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

                content.Add(streamContent, "file", file.FileName);

                var response = await _httpClient.PostAsync($"/api/corregedoria/uploadcsv?ps={pageSize}&page={pageIndex}", content);

                TratarErrosResponse(response);

                return await DeserializarObjetoResponse<IEnumerable<InstauracaoDTO>>(response);
            }
        }


    }
}
