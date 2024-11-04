using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class AcordoController : Controller
    {
        private readonly IAcordoLenienciaService _acordoLenienciaService;
        public AcordoController(IAcordoLenienciaService acordoLenienciaService)
        {
            _acordoLenienciaService = acordoLenienciaService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _acordoLenienciaService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }
        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var acordo = await _acordoLenienciaService.ObterId(id);

            return View(acordo);
        }


        public IActionResult IndexConsulta()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var acordo = await _acordoLenienciaService.BuscarCNPJ(cnpj);
            return View(acordo);
        }


    }
}
