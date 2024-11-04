using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Compras;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Compras
{
    public class DispensaController : MainController
    {
        private readonly IDispensaService _dispensaService;

        public DispensaController(IDispensaService dispensaService)
        {
            _dispensaService = dispensaService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _dispensaService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }


        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var dispensa = await _dispensaService.ObterId(id);

            return View(dispensa);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var dispensa = await _dispensaService.BuscarCNPJ(cnpj);
            return View(dispensa);
        }





    }
}
