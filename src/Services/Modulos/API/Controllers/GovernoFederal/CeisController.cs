using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.GovernoFederal.Controllers
{
    [ApiController]
    [Route("api/ceis")]
    [Authorize]
    public class CeisController : Controller
    {
        private readonly ICeisRepositoryRead _ceisRepositoryRead;


        public CeisController(ICeisRepositoryRead ceisRepositoryRead)
        {
            _ceisRepositoryRead = ceisRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _ceisRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar/{id}")]
        public async Task<Ceis> BuscaId(Guid id)
        {
            return await _ceisRepositoryRead.ObterId(id);
        }


        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarPorCNPJ(string cnpj)
        {
            var ceis = await _ceisRepositoryRead.BuscarCNPJ(cnpj);

            if (ceis == null || !ceis.Any())
            {
                return NotFound("Nenhuma ocorrencia encontrado para o CNPJ fornecido.");
            }

            return Ok(ceis);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var ceis = await _ceisRepositoryRead.BuscarCPF(cpf);

            if (ceis == null || !ceis.Any())
            {
                return NotFound("Nenhuma ocorrencia encontrado para o CPF fornecido.");
            }

            return Ok(ceis);
        }

    }
}
