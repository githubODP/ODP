using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Corregedoria;
using ODP.Web.UI.Models.DueDiligence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
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

        // Novo método para listar com filtros e/ou protocolo
        public async Task<PagedResult<InstauracaoViewModel>> ListarComFiltros(
            int pageNumber = 1,
            int pageSize = 10,
            int? ano = null,
            string orgao = null,
            string procedimento = null,
            string decisao = null,
            string protocolo = null) // Protocolo incluído como opcional
        {
            // Monta a query string com filtros opcionais
            var queryParams = new List<string>
            {
                $"pageNumber={pageNumber}",
                $"pageSize={pageSize}"
            };

            if (!string.IsNullOrEmpty(protocolo))
            {
                queryParams.Add($"protocolo={protocolo}"); // Filtro por protocolo
            }
            else
            {
                // Adiciona os filtros tradicionais se protocolo não foi fornecido
                if (ano.HasValue)
                    queryParams.Add($"ano={ano.Value}");

                if (!string.IsNullOrEmpty(orgao))
                    queryParams.Add($"orgao={orgao}");

                if (!string.IsNullOrEmpty(procedimento))
                    queryParams.Add($"procedimento={procedimento}");

                if (!string.IsNullOrEmpty(decisao))
                    queryParams.Add($"decisao={decisao}");
            }

            // Combina todos os parâmetros em uma query string
            var queryString = string.Join("&", queryParams);

            // Faz a requisição para a API
            var response = await _httpClient.GetAsync($"/api/corregedoria/listar?{queryString}");

            // Trata erros de resposta
            TratarErrosResponse(response);

            // Deserializa e retorna o resultado
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
                return await DeserializarObjetoResponse<InstauracaoViewModel>(response);

            }
            return null;

        }

        public async Task<InstauracaoViewModel> Alterar(InstauracaoViewModel instauracaoViewModel, Guid id)
        {

            var instauracaoContent = ObterConteudo(instauracaoViewModel);

            var response = await _httpClient.PostAsync($"/api/corregedoria/alterar/{id}", instauracaoContent);

            if (TratarErrosResponse(response))
            {
                return null;
            }
            return await DeserializarObjetoResponse<InstauracaoViewModel>(response);


        }


        public async Task<InstauracaoViewModel> Deletar(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/corregedoria/deletar/{id}");            

            if (TratarErrosResponse(response))
            {
                return null;
            }
            return null;
        }




        //public async Task<FileStreamResult> GerarPdf(Guid id)
        //{
        //    var response = await _httpClient.GetAsync($"api/corregedoria/gerarpdf/{id}");



        //    if (response.IsSuccessStatusCode)
        //    {

        //        var pdfContent = await response.Content.ReadAsByteArrayAsync();


        //        var memoryStream = new MemoryStream(pdfContent);


        //        return new FileStreamResult(memoryStream, "application/pdf")
        //        {
        //            FileDownloadName = "elemento.pdf"
        //        };
        //    }
        //    else
        //    {

        //        return null;
        //    }
        //}

        //public async Task<bool> UploadCsv(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        throw new ArgumentException("Arquivo CSV inválido.", nameof(file));

        //    try
        //    {
        //        using (var content = new MultipartFormDataContent())
        //        {
        //            // Ler o conteúdo do arquivo e garantir que esteja em UTF-8
        //            string utf8Content;
        //            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.Default)) // Encoding.Default lida com arquivos em ANSI ou ISO
        //            {
        //                var fileContent = reader.ReadToEnd();
        //                utf8Content = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(fileContent));
        //            }

        //            // Criar um MemoryStream a partir do conteúdo UTF-8
        //            var utf8Stream = new MemoryStream(Encoding.UTF8.GetBytes(utf8Content));

        //            // Adicionar o arquivo como StreamContent
        //            var streamContent = new StreamContent(utf8Stream);
        //            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

        //            content.Add(streamContent, "file", file.FileName);

        //            var response = await _httpClient.PostAsync("/api/corregedoria/uploadcsv", content);

        //            TratarErrosResponse(response); // Verifica erros na resposta.

        //            return response.IsSuccessStatusCode;
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        throw new InvalidOperationException("Erro ao enviar o arquivo CSV. Verifique a conexão com o backend.", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException("Ocorreu um erro inesperado durante o upload do arquivo CSV.", ex);
        //    }
        //}

        public async Task<bool> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo CSV inválido.", nameof(file));

            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    var streamContent = new StreamContent(file.OpenReadStream());
                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

                    content.Add(streamContent, "file", file.FileName);
                    var response = await _httpClient.PostAsync("/api/corregedoria/uploadcsv", content);

                    TratarErrosResponse(response);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Erro ao enviar o arquivo CSV.", ex);
            }
        }







    }
}
