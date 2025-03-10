using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.DueDiligence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ODP.Web.UI.Services.DueDiligence
{


    public class DueService : Service, IDueService
    {
        private readonly HttpClient _httpClient;

        public DueService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);

            _httpClient = httpClient;
        }


        public async Task<PagedResult<DueDiligenceViewModel>> Listar(int pageNumber = 1, int pageSize = 5, string termo = null)
        {
            var queryParams = new List<string>
            {
                $"pageNumber={pageNumber}",
                $"pageSize={pageSize}"
    };

            if (!string.IsNullOrEmpty(termo))
            {
                queryParams.Add($"termo={termo}"); // Enviando um único termo para busca dinâmica
            }

            // Combina todos os parâmetros em uma query string
            var queryString = string.Join("&", queryParams);

            var response = await _httpClient.GetAsync($"/api/duediligence/listar?{queryString}");


            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedResult<DueDiligenceViewModel>>(response);
        }



        public async Task<DueDiligenceViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/duediligence/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<DueDiligenceViewModel>(response);
        }

        public async Task<DueDiligenceViewModel> Adicionar(DueDiligenceViewModel dueViewModel)
        {
            var dueContent = ObterConteudo(dueViewModel);
            var response = await _httpClient.PostAsync("/api/duediligence/adicionar", dueContent);
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<DueDiligenceViewModel>(response);



        }

        public async Task<DueDiligenceViewModel> Alterar(DueDiligenceViewModel dueViewModel, Guid id)
        {

            var dueContent = ObterConteudo(dueViewModel);

            var response = await _httpClient.PutAsync("/api/duediligence/alterar", dueContent);

            if (TratarErrosResponse(response))
            {
                return null;

            }
            return await DeserializarObjetoResponse<DueDiligenceViewModel>(response);

        }



        public async Task<FileStreamResult> GerarPdf(DueDiligenceViewModel due)
        {

            var jsonContent = JsonConvert.SerializeObject(due);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("/api/duediligence/gerarpdf", content);

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






        public async Task<DueDiligenceViewModel> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/duediligence/deletar/{id}");
            TratarErrosResponse(response);

            if (TratarErrosResponse(response))
            {
                return null;
            }
            return null;
        }



        public async Task<FileStreamResult> GerarPdf(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/duediligence/gerarpdf/{id}");



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


        public async Task<List<DueDiligenceViewModel>> BuscarPorCPF(string cpf)
        {

            try
            {
                var response = await _httpClient.GetAsync($"api/duediligence/consultacpf/{cpf}");
                TratarErrosResponse(response);

                // Verificar se a resposta contém conteúdo
                if (response.Content == null || response.Content.Headers.ContentLength == 0)
                {
                    return new List<DueDiligenceViewModel>(); // Retorna um modelo vazio
                }

                var contrato = await DeserializarObjetoResponse<List<DueDiligenceViewModel>>(response);
                return contrato;
            }
            catch (CustomHttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Retornar um modelo vazio se o contrato não for encontrado (404)
                return new List<DueDiligenceViewModel>();
            }
        }



    }
}
