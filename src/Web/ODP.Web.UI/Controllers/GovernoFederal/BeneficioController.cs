using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class BeneficioController : MainController
    {
        private readonly IBeneficioContinuoService _beneficioContinuoService;

        public BeneficioController(IBeneficioContinuoService beneficioContinuoService)
        {
            _beneficioContinuoService = beneficioContinuoService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _beneficioContinuoService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var beneficio = await _beneficioContinuoService.ObterId(id);

            return View(beneficio);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var beneficio = await _beneficioContinuoService.BuscarCPF(cpf);
            return View(beneficio);
        }

    }
}
