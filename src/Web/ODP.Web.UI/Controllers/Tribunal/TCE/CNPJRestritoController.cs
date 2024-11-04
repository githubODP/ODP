using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCE;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Tribunal.TCE
{
    public class CNPJRestritoController : MainController
    {
        private readonly ICNPJRestritoService _cnpjrestritoService;

        public CNPJRestritoController(ICNPJRestritoService cnpjrestritoService)
        {
            _cnpjrestritoService = cnpjrestritoService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _cnpjrestritoService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var restrito = await _cnpjrestritoService.ObterId(id);

            return View(restrito);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var restrito = await _cnpjrestritoService.BuscarCNPJ(cnpj);
            return View(restrito);
        }

    }
}
