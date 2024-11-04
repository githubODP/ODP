using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Dividas;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Dividas
{
    public class DividaNaoPrevidenciaController : MainController
    {
        private readonly IDividaNaoPrevidenciaService _dividanaoprevidenciaService;

        public DividaNaoPrevidenciaController(IDividaNaoPrevidenciaService dividanaoprevidenciaService)
        {
            _dividanaoprevidenciaService = dividanaoprevidenciaService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _dividanaoprevidenciaService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var divida = await _dividanaoprevidenciaService.ObterId(id);

            return View(divida);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var divida = await _dividanaoprevidenciaService.BuscarCNPJ(cnpj);
            return View(divida);
        }

    }
}
