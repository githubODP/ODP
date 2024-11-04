using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.GovernoFederal
{
    [ApiController]
    [Route("api/pep")]
    [Authorize]
    public class PEPController : Controller
    {
        private readonly IPEPRepositoryRead _pepRepositoryRead;


        public PEPController(IPEPRepositoryRead pepRepositoryRead)
        {
            _pepRepositoryRead = pepRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _pepRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar/{id}")]
        public async Task<PEP> BuscaId(Guid id)
        {
            return await _pepRepositoryRead.ObterId(id);
        }


        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var ocorrencia = await _pepRepositoryRead.BuscarCPF(cpf);

            if (ocorrencia == null || !ocorrencia.Any())
            {
                return NotFound("Nenhuma ocorrência encontrado para o CPF fornecido.");
            }

            return Ok(ocorrencia);
        }

    }
}
