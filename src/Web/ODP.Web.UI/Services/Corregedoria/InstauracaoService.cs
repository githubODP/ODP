using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Corregedoria;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace ODP.Web.UI.Services.Corregedoria
{
    public class InstauracaoService : Service, IInstauracaoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<InstauracaoService> _logger;

        public InstauracaoService(ILogger<InstauracaoService> logger, HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            _logger = logger;
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
            _logger.LogInformation("Iniciando ListarComFiltros com parâmetros: pageNumber={PageNumber}, pageSize={PageSize}, ano={Ano}, orgao={Orgao}, procedimento={Procedimento}, decisao={Decisao}, protocolo={Protocolo}", pageNumber, pageSize, ano, orgao, procedimento, decisao, protocolo);
            // Monta a query string com filtros opcionais
            var queryParams = new List<string>
            {
                $"pageNumber={pageNumber}",
                $"pageSize={pageSize}"
            };

            if (!string.IsNullOrEmpty(protocolo))
            {
                queryParams.Add($"protocolo={protocolo}"); // Filtro por protocolo
                _logger.LogDebug("Filtro por protocolo aplicado.");
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
            try
            {
                var response = await _httpClient.GetAsync($"/api/corregedoria/listar?{string.Join("&", queryParams)}");

                _logger.LogInformation("Recebeu resposta da API com status {StatusCode}.", response.StatusCode);

                TratarErrosResponse(response);

                return await DeserializarObjetoResponse<PagedResult<InstauracaoViewModel>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar ListarComFiltros no serviço.");
                throw;
            }
        }







        public async Task<InstauracaoViewModel> ObterId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/corregedoria/buscaId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<InstauracaoViewModel>(response);
        }

        public async Task<InstauracaoViewModel> Adicionar(InstauracaoViewModel instauracaoViewModel)
        {
            var instauracaoContent = ObterConteudo(instauracaoViewModel);

            var response = await _httpClient.PostAsync("/api/corregedoria/adicionar", instauracaoContent);

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
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<InstauracaoViewModel>(response);

        }


        public async Task<bool> Deletar(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/corregedoria/excluir/{id}");
                return TratarErrosResponse(response); // Usa o método TratarErrosResponse
            }
            catch (CustomHttpRequestException ex)
            {
                // Log da exceção personalizada
                Console.WriteLine($"Erro ao excluir registro: {ex.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                // Log de exceções genéricas
                Console.WriteLine($"Erro inesperado ao excluir registro: {ex.Message}");
                return false;
            }
        }


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
