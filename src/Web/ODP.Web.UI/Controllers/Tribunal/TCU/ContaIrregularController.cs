using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCU;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Tribunal.TCU
{
    public class ContaIrregularController : MainController
    {
        private readonly IContaIrregularService _contaService;

        public ContaIrregularController(IContaIrregularService contaService)
        {
            _contaService = contaService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _contaService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var conta = await _contaService.ObterId(id);

            return View(conta);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var conta = await _contaService.BuscarCNPJ(cnpj);
            return View(conta);
        }

    }
}
