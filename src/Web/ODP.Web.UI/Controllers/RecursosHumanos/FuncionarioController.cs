using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.RecursosHumanos;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.RecursosHumanos
{
    public class FuncionarioController : MainController
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var funcionario = await _funcionarioService.Listar(pageNumber, pageSize);
            return View(funcionario);
        }



        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var funcionario = await _funcionarioService.ObterId(id);

            return View(funcionario);
        }

        public IActionResult IndexConsulta()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var funcionario = await _funcionarioService.BuscarCNPJ(cnpj);
            return View(funcionario);
        }



        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var contrato = await _funcionarioService.BuscarCPF(cpf);
            return View(contrato);
        }



    }

}
