using Domain.Graficos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/graficos")]
    [Authorize]
    public class GraficoController : Controller
    {

        private readonly IGraficosRepositoryRead _graficosRepositoryRead;
        

        public GraficoController(IGraficosRepositoryRead graficosRepositoryRead)
        {
            _graficosRepositoryRead = graficosRepositoryRead;
        }

       [HttpGet("GraficoODP")]

       public async Task<IActionResult> GraficoODP()
        {
            var result = await _graficosRepositoryRead.GraficoOdp();
            return Ok(result);
        }
    }
}
