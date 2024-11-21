using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class BolsaController : Controller
    {
        private readonly IBolsaService _bolsaService;
        public BolsaController(IBolsaService bolsaService)
        {
            _bolsaService = bolsaService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _bolsaService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }
        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var bolsa = await _bolsaService.ObterId(id);

            return View(bolsa);
        }


        public IActionResult IndexConsulta()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var bolsa = await _bolsaService.BuscarCNPJ(cnpj);
            return View(bolsa);
        }
    }
}
