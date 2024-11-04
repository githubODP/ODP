using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCE;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Tribunal.TCE
{
    public class InadimplenteController : MainController
    {
        private readonly IInadimplenteService _inadimplenteService;

        public InadimplenteController(IInadimplenteService inadimplenteService)
        {
            _inadimplenteService = inadimplenteService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _inadimplenteService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var inadimplente = await _inadimplenteService.ObterId(id);

            return View(inadimplente);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var inadimplente = await _inadimplenteService.BuscarCNPJ(cnpj);
            return View(inadimplente);
        }

    }
}
