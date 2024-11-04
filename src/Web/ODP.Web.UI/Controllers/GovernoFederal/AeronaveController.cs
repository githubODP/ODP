using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.GovernoFederal
{
    public class AeronaveController : MainController
    {
        private readonly IAeronaveService _aeronaveService;

        public AeronaveController(IAeronaveService aeronaveService)
        {
            _aeronaveService = aeronaveService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _aeronaveService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var aeronave = await _aeronaveService.ObterId(id);

            return View(aeronave);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var aeronave = await _aeronaveService.BuscarCNPJ(cnpj);
            return View(aeronave);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var aeronave = await _aeronaveService.BuscarCPF(cpf);
            return View(aeronave);
        }
    }
}
