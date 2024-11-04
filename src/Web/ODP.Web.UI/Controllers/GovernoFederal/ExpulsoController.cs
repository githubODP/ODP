using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class ExpulsoController : MainController
    {
        private readonly IExpulsoFederalService _expulsoService;

        public ExpulsoController(IExpulsoFederalService expulsoService)
        {
            _expulsoService = expulsoService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _expulsoService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var expulso = await _expulsoService.ObterId(id);

            return View(expulso);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var expulso = await _expulsoService.BuscarCPF(cpf);
            return View(expulso);
        }

    }
}
