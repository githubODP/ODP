using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Compras;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Compras
{
    public class ContratoController : MainController
    {
        private readonly IContratoService _contratoService;

        public ContratoController(IContratoService contratoService)
        {
            _contratoService = contratoService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var contrato = await _contratoService.Listar(pageNumber, pageSize);
            return View(contrato);
        }



        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var contrato = await _contratoService.ObterId(id);

            return View(contrato);
        }

        public IActionResult IndexConsulta()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var contrato = await _contratoService.BuscarCNPJ(cnpj);
            return View(contrato);
        }



        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var contrato = await _contratoService.BuscarCPF(cpf);
            return View(contrato);
        }



    }

}
