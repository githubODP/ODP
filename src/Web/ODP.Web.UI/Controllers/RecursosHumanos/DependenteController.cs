using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Compras;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.RecursosHumanos
{
    public class DependenteController : MainController
    {
        private readonly IContratoService _dependenteService;

        public DependenteController(IContratoService dependenteService)
        {
            _dependenteService = dependenteService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var dependente = await _dependenteService.Listar(pageNumber, pageSize);
            return View(dependente);
        }



        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var dependente = await _dependenteService.ObterId(id);

            return View(dependente);
        }

        public IActionResult IndexConsulta()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var contrato = await _dependenteService.BuscarCNPJ(cnpj);
            return View(contrato);
        }



        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var contrato = await _dependenteService.BuscarCPF(cpf);
            return View(contrato);
        }



    }

}
