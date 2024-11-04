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
    [Route("api/beneficio")]
    [Authorize]
    public class BeneficioController : Controller
    {
        private readonly IBeneficioContinuoRepositoryRead _beneficioRepositoryRead;


        public BeneficioController(IBeneficioContinuoRepositoryRead beneficioRepositoryRead)
        {
            _beneficioRepositoryRead = beneficioRepositoryRead;

        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _beneficioRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("listar/{id}")]
        public async Task<BeneficioContinuo> BuscaId(Guid id)
        {
            return await _beneficioRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var beneficio = await _beneficioRepositoryRead.BuscarCPF(cpf);

            if (beneficio == null || !beneficio.Any())
            {
                return NotFound("Nenhum beneficio encontrado para o CPF fornecido.");
            }

            return Ok(beneficio);
        }

    }
}
