using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Dividas;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Dividas
{
    public class DividaPrevidenciaController : MainController
    {
        private readonly IDividaPrevidenciaService _dividaPrevidenciaService;

        public DividaPrevidenciaController(IDividaPrevidenciaService dividaPrevidenciaService)
        {
            _dividaPrevidenciaService = dividaPrevidenciaService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _dividaPrevidenciaService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var divida = await _dividaPrevidenciaService.ObterId(id);

            return View(divida);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var divida = await _dividaPrevidenciaService.BuscarCNPJ(cnpj);
            return View(divida);
        }

    }
}
