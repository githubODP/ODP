using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Fazenda;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Fazenda
{
    public class NFEletronicaFederalController : MainController
    {
        private readonly INFEletronicaFederalService _nfeletronicaService;

        public NFEletronicaFederalController(INFEletronicaFederalService nfeletronicaService)
        {
            _nfeletronicaService = nfeletronicaService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _nfeletronicaService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var contrato = await _nfeletronicaService.ObterId(id);

            return View(contrato);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var contrato = await _nfeletronicaService.BuscarCNPJ(cnpj);
            return View(contrato);
        }

    }
}
