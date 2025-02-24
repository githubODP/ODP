using CGEODP.WebApi.Core.Identidade;
using Domain.Compras.Entidades;
using Domain.Compras.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Compras.API.Controllers
{
    [ApiController]
    [Route("api/contratos")]
    [Authorize]

    public class ContratoController : Controller
    {

        private readonly IContratoRepositoryRead _contratoRepositoryRead;
        public ContratoController(IContratoRepositoryRead contratoRepositoryRead)
        {
            _contratoRepositoryRead = contratoRepositoryRead;
        }
        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _contratoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("buscaId/{id}")]
        public async Task<Contrato> BuscaId(Guid id)
        {
            return await _contratoRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarPorCNPJ(string cnpj)
        {
            var contratos = await _contratoRepositoryRead.BuscarCNPJ(cnpj);

            if (contratos == null || !contratos.Any())
            {
                return NotFound("Nenhum contrato encontrado para o CNPJ fornecido.");
            }

            return Ok(contratos);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var contratos = await _contratoRepositoryRead.BuscarCPF(cpf);

            if (contratos == null || !contratos.Any())
            {
                return NotFound("Nenhum contrato encontrado para o CPF fornecido.");
            }

            return Ok(contratos);
        }




    }
}
