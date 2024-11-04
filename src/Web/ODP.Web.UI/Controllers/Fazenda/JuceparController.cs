using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoEstadual;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Fazenda
{
    public class JuceparController : MainController
    {
        private readonly IPADVService _padvService;

        public JuceparController(IPADVService padvService)
        {
            _padvService = padvService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _padvService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var contrato = await _padvService.ObterId(id);

            return View(contrato);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var contrato = await _padvService.BuscarCNPJ(cnpj);
            return View(contrato);
        }

    }
}
