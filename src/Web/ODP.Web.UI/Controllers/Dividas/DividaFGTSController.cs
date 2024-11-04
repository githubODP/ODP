using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Dividas;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Dividas
{
    public class DividaFGTSController : MainController
    {
        private readonly IDividaFGTsService _dividafgtsService;

        public DividaFGTSController(IDividaFGTsService dividafgtsService)
        {
            _dividafgtsService = dividafgtsService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _dividafgtsService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var divida = await _dividafgtsService.ObterId(id);

            return View(divida);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var divida = await _dividafgtsService.BuscarCNPJ(cnpj);
            return View(divida);
        }

    }
}
