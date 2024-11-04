using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCU;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Tribunal.TCU
{
    public class InabilitadoController : MainController
    {
        private readonly IInabilitadoService _inabilitadoService;

        public InabilitadoController(IInabilitadoService inabilitadoService)
        {
            _inabilitadoService = inabilitadoService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _inabilitadoService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var inabilitado = await _inabilitadoService.ObterId(id);

            return View(inabilitado);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var inabilitado = await _inabilitadoService.BuscarCPF(cpf);
            return View(inabilitado);
        }

    }
}
