using Domain.Tribunal.Entidades.TCU;
using Domain.Tribunal.Interfaces.TCU;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Tribunais.TCU
{
    [ApiController]
    [Route("api/contairregular")]
    [Authorize]
    public class ContaIrregularController : Controller
    {
        private readonly IContaIrregularRepositoryRead _contaIrregularRepositoryread;

        public ContaIrregularController(IContaIrregularRepositoryRead contaIrregularRepository)
        {
            _contaIrregularRepositoryread = contaIrregularRepository;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _contaIrregularRepositoryread.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<ContaIrregular> BuscaId(Guid id)
        {
            return await _contaIrregularRepositoryread.ObterId(id);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarCPF(string cpf)
        {
            var conta = await _contaIrregularRepositoryread.BuscarCPF(cpf);
            if (conta == null || !conta.Any())
            {
                return NotFound("Nenhum conta encontrado para o CPF fornecido.");
            }

            return Ok(conta);
        }


        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarCNPJ(string cnpj)
        {
            var conta = await _contaIrregularRepositoryread.BuscarCNPJ(cnpj);

            if (conta == null || !conta.Any())
            {
                return NotFound("Nenhum conta encontrado para o CPF fornecido.");
            }

            return Ok(conta);
        }


    }
}
