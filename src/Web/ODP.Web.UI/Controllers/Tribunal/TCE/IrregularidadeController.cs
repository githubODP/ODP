using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCE;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Tribunal.TCE
{
    public class IrregularidadeController : MainController
    {
        private readonly IIrregularidadeService _irregulariddeService;

        public IrregularidadeController(IIrregularidadeService irregulariddeService)
        {
            _irregulariddeService = irregulariddeService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _irregulariddeService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var irregular = await _irregulariddeService.ObterId(id);

            return View(irregular);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var irregular = await _irregulariddeService.BuscarCPF(cpf);
            return View(irregular);
        }

    }
}
