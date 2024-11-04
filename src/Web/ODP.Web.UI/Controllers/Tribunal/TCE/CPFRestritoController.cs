using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCE;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Tribunal.TCE
{
    public class CPFRestritoController : MainController
    {
        private readonly ICPFRestritoService _cpfrestritoService;

        public CPFRestritoController(ICPFRestritoService cpfrestritoService)
        {
            _cpfrestritoService = cpfrestritoService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _cpfrestritoService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var restrito = await _cpfrestritoService.ObterId(id);

            return View(restrito);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var restrito = await _cpfrestritoService.BuscarCPF(cpf);
            return View(restrito);
        }

    }
}
