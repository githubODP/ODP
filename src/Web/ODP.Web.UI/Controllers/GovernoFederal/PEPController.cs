using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class PEPController : MainController
    {
        private readonly IPEPService _pepService;

        public PEPController(IPEPService pepService)
        {
            _pepService = pepService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _pepService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var pep = await _pepService.ObterId(id);

            return View(pep);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var pep = await _pepService.BuscarCPF(cpf);
            return View(pep);
        }

    }
}
