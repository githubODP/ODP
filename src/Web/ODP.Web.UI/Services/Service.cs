using ODP.Web.UI.Extensions;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services
{
    public abstract class Service
    {
        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false) }
                };

                var content = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"Conteúdo recebido: {content}"); // Log do JSON completo

                // Log do conteúdo recebido para análise
                //Console.WriteLine($"Conteúdo recebido para deserialização: {content}");

                return JsonSerializer.Deserialize<T>(content, options);
            }
            catch (Exception ex)
            {
                // Log do erro de deserialização
                Console.WriteLine($"Erro ao deserializar a resposta: {ex.Message}");
                throw;
            }
        }



        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                // Captura detalhes do erro para diagnóstico
                var statusCode = (int)response.StatusCode;
                var errorContent = response.Content.ReadAsStringAsync().Result;

                // Log dos detalhes do erro (pode ser substituído por qualquer método de logging da sua aplicação)
                Console.WriteLine($"Erro HTTP {statusCode}: {response.ReasonPhrase}");
                Console.WriteLine($"Conteúdo da Resposta: {errorContent}");

                // Trata os erros conforme já implementado
                switch (statusCode)
                {
                    case 401:
                    case 403:
                    case 404:
                    case 500:
                        throw new CustomHttpRequestException(response.StatusCode);

                    case 400:
                        return false;
                }
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

    }
}
