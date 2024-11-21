using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class CnepController : Controller
    {
        private readonly ICnepService _cnepService;
        public CnepController(ICnepService cnepService)
        {
            _cnepService = cnepService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _cnepService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }
        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var cnep = await _cnepService.ObterId(id);

            return View(cnep);
        }


        public IActionResult IndexConsulta()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var cnep = await _cnepService.BuscarCNPJ(cnpj);
            return View(cnep);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var cnep = await _cnepService.BuscarCPF(cpf);
            return View(cnep);
        }
    }
}
