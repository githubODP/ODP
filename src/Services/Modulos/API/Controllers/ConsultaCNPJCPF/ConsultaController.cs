using Domain.ConsultaCNPJCPF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.ConsultaCNPJCPF
{
    [ApiController]
    [Route("api/busca")]
    [Authorize]

    public class ConsultaController : Controller
    {
        private readonly IBuscaService _buscaService;

        public ConsultaController(IBuscaService buscaService)
        {
            _buscaService = buscaService;
        }


        [HttpPost("consultacnpj")]
        public async Task<IActionResult> ConsultaCNPJ([FromQuery] string cnpj, [FromQuery] List<string> tabelasSelecionadas)
        {
            // Log para garantir que os dados estão chegando
            Console.WriteLine($"CNPJ: {cnpj}");
            Console.WriteLine($"Tabelas: {string.Join(", ", tabelasSelecionadas ?? new List<string>())}");

            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return BadRequest("CNPJ é obrigatório.");
            }

            var resultados = new List<object>();

            await foreach (var item in _buscaService.BuscarCNPJ(cnpj, tabelasSelecionadas))
            {
                resultados.Add(item);
            }

            return Ok(resultados);
        }


        [HttpPost("consultacpf")]
        public async Task<IActionResult> ConsultaCPF([FromQuery] string cpf, [FromQuery] List<string> tabelasSelecionadas)
        {
            // Log para garantir que os dados estão chegando
            Console.WriteLine($"CPF: {cpf}");
            Console.WriteLine($"Tabelas: {string.Join(", ", tabelasSelecionadas ?? new List<string>())}");

            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("CPF é obrigatório.");
            }

            var resultados = new List<object>();

            await foreach (var item in _buscaService.BuscarCPF(cpf, tabelasSelecionadas))
            {
                resultados.Add(item);
            }

            return Ok(resultados);
        }



    }

}
