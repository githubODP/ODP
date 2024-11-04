using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class TrabalhoController : MainController
    {
        private readonly ITrabalhoEscravoService _trabalhoService;

        public TrabalhoController(ITrabalhoEscravoService trabalhoService)
        {
            _trabalhoService = trabalhoService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _trabalhoService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var trabalho = await _trabalhoService.ObterId(id);

            return View(trabalho);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var trabalho = await _trabalhoService.BuscarCNPJ(cnpj);
            return View(trabalho);
        }

    }
}
