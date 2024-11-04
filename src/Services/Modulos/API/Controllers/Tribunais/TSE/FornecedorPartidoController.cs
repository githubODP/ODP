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
    [Route("api/fornecedorpartido")]
    [Authorize]
    public class FornecedorPartidoController : Controller
    {
        private readonly IFornecedorPartidoRepositoryRead _fornecedorPartidoRepositoryRead;

        public FornecedorPartidoController(IFornecedorPartidoRepositoryRead fornecedorPartidoRepositoryRead)
        {
            _fornecedorPartidoRepositoryRead = fornecedorPartidoRepositoryRead;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _fornecedorPartidoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<FornecedorPartido> BuscaId(Guid id)
        {
            return await _fornecedorPartidoRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarCNPJ(string cnpj)
        {
            var fornecedor = await _fornecedorPartidoRepositoryRead.BuscarCNPJ(cnpj);
            if (fornecedor == null || !fornecedor.Any())
            {
                return NotFound("Nenhum fornecedor encontrado para o CNPJ fornecido.");
            }

            return Ok(fornecedor);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarCPF(string cpf)
        {
            var fornecedor = await _fornecedorPartidoRepositoryRead.BuscarCPF(cpf);

            if (fornecedor == null || !fornecedor.Any())
            {
                return NotFound("Nenhum fornecedor encontrado para o CPF fornecido.");
            }

            return Ok(fornecedor);
        }
    }
}
