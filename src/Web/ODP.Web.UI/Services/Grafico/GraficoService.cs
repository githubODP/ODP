using Microsoft.Extensions.Options;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Graficos;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Grafico
{
    public class GraficoService : Service, IGraficoService
    {
        private readonly HttpClient _httpClient;
        public GraficoService(HttpClient httpClient, IOptions<AppSettings> settings) {
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri(settings.Value.ModulosUrl);
        }

        public async Task<GraficoViewModel> GraficoODP()
        {
            var response = await _httpClient.GetAsync($"api/graficos/GraficoODP");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<GraficoViewModel>(response);
        }
    }
    
}
