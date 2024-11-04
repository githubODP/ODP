using Domain.Tribunal.Entidades.TSE;
using Domain.Tribunal.Interfaces.TSE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Tribunais.TSE
{
    [ApiController]
    [Route("api/fornecedorcandidato")]
    [Authorize]
    public class FornecedorCandidatoController : Controller
    {
        private readonly IFornecedorCandidatoRepositoryRead _fornecedorCandidatoRepositoryRead;

        public FornecedorCandidatoController(IFornecedorCandidatoRepositoryRead fornecedorCandidatoRepositoryRead)
        {
            _fornecedorCandidatoRepositoryRead = fornecedorCandidatoRepositoryRead;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _fornecedorCandidatoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<FornecedorCandidato> BuscaId(Guid id)
        {
            return await _fornecedorCandidatoRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarCNPJ(string cnpj)
        {
            var fornecedor = await _fornecedorCandidatoRepositoryRead.BuscarCNPJ(cnpj);

            if (fornecedor == null || !fornecedor.Any())
            {
                return NotFound("Nenhum Fornecedor encontrado para o CNPJ fornecido.");
            }

            return Ok(fornecedor);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarCPF(string cpf)
        {
            var fornecedor = await _fornecedorCandidatoRepositoryRead.BuscarCPF(cpf);


            if (fornecedor == null || !fornecedor.Any())
            {
                return NotFound("Nenhum Fornecedor encontrado para o CNPJ fornecido.");
            }

            return Ok(fornecedor);
        }
    }
}
