using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class CeisController : MainController
    {
        private readonly ICeisService _ceisService;

        public CeisController(ICeisService ceisService)
        {
            _ceisService = ceisService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _ceisService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var ceis = await _ceisService.ObterId(id);

            return View(ceis);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var ceis = await _ceisService.BuscarCNPJ(cnpj);
            return View(ceis);
        }

    }
}
