using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class SeguroController : MainController
    {
        private readonly ISeguroDefesoService _seguroService;

        public SeguroController(ISeguroDefesoService seguroService)
        {
            _seguroService = seguroService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _seguroService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var seguro = await _seguroService.ObterId(id);

            return View(seguro);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var seguro = await _seguroService.BuscarCPF(cpf);
            return View(seguro);
        }

    }
}
