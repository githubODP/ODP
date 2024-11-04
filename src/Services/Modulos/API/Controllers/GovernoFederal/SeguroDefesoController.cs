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
    [Route("api/segurodefeso")]
    [Authorize]
    public class SeguroDefesoController : Controller
    {
        private readonly ISeguroDefesoRepositoryRead _seguroRepositoryRead;


        public SeguroDefesoController(ISeguroDefesoRepositoryRead seguroRepositoryRead)
        {
            _seguroRepositoryRead = seguroRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _seguroRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar/{id}")]
        public async Task<SeguroDefeso> BuscaId(Guid id)
        {
            return await _seguroRepositoryRead.ObterId(id);
        }



        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var seguro = await _seguroRepositoryRead.BuscarCPF(cpf);

            if (seguro == null || !seguro.Any())
            {
                return NotFound("Nenhum seguro encontrado para o CPF fornecido.");
            }

            return Ok(seguro);
        }

    }
}
