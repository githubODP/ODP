using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoEstadual;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoEstadual
{
    public class AmbientalController : MainController
    {
        private readonly IAmbientalService _ambientalService;

        public AmbientalController(IAmbientalService ambientalService)
        {
            _ambientalService = ambientalService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _ambientalService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var ambiental = await _ambientalService.ObterId(id);

            return View(ambiental);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var ambiental = await _ambientalService.BuscarCNPJ(cnpj);
            return View(ambiental);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpF)
        {
            var ambiental = await _ambientalService.BuscarCPF(cpF);
            return View(ambiental);
        }

    }
}
