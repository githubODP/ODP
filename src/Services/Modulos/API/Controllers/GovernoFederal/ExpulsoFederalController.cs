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
    [Route("api/expulsofederal")]
    [Authorize]
    public class ExpulsoFederalController : Controller
    {
        private readonly IExpulsoFederalRepositoryRead _expulsoRepositoryRead;


        public ExpulsoFederalController(IExpulsoFederalRepositoryRead expulsoRepositoryRead)
        {
            _expulsoRepositoryRead = expulsoRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _expulsoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("consulta/{id}")]
        public async Task<ExpulsoFederal> BuscaId(Guid id)
        {
            return await _expulsoRepositoryRead.ObterId(id);
        }




        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var expulso = await _expulsoRepositoryRead.BuscarCPF(cpf);

            if (expulso == null || !expulso.Any())
            {
                return NotFound("Nenhuma ocorrencia encontrado para o CPF fornecido.");
            }

            return Ok(expulso);
        }



    }
}
