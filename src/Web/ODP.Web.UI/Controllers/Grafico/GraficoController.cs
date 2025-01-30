using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ODP.Web.UI.Models.Graficos;
using ODP.Web.UI.Services.Grafico;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Grafico
{
    public class GraficoController : MainController
    {
        private readonly IGraficoService _graficoService;


        public GraficoController(IGraficoService graficoService)
        {
            _graficoService = graficoService;
        }


        [HttpGet]
        public async Task<IActionResult> GraficoODP()
        {
            var response = await _graficoService.GraficoODP();

            if (response != null && !string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    // Tenta deserializar como um array JSON (o caso do seu exemplo)
                    JArray jsonArray = JArray.Parse(response.Content);

                    var viewModel = new GraficoViewModel { Content = jsonArray.ToString() }; // Armazena a string JSON formatada

                    return View(viewModel);
                }
                catch (JsonReaderException exArray)
                {
                    try
                    {
                        // Se falhar como array, tenta deserializar como um objeto JSON
                        JObject jsonObject = JObject.Parse(response.Content);

                        var viewModel = new GraficoViewModel { Content = jsonObject.ToString() }; // Armazena a string JSON formatada
                        return View(viewModel);

                    }
                    catch (JsonReaderException exObject)
                    {
                        // Se nenhum dos dois funcionar, lida com o erro
                        ModelState.AddModelError("", $"Erro ao processar dados da API: {exArray.Message} ou {exObject.Message}");
                        return View(new GraficoViewModel());
                    }

                }
            }
            else
            {
                ModelState.AddModelError("", "Resposta da API vazia ou nula.");
                return View(new GraficoViewModel());
            }
        }
    }
}
