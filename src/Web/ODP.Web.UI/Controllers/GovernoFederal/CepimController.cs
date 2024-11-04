using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System.Threading.Tasks;
using System;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class CepimController : Controller
    {
        private readonly ICepimService _cepimService;
        public CepimController(ICepimService cepimService)
        {
            _cepimService = cepimService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _cepimService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }
        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var cepim = await _cepimService.ObterId(id);

            return View(cepim);
        }


        public IActionResult IndexConsulta()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var cepim = await _cepimService.BuscarCNPJ(cnpj);
            return View(cepim);
        }
    }
}
