
using Domain.RecursosHumanos.Entidades;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace RH.API.Controllers
{
    [ApiController]
    [Route("api/dependentes")]
    [Authorize]
    public class DependentesController : Controller
    {
        private readonly IDependenteRepositoryRead _dependentesRepositoryRead;


        public DependentesController(IDependenteRepositoryRead repositoryRead)
        {

            _dependentesRepositoryRead = repositoryRead;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _dependentesRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("buscaId/{id}")]
        public async Task<Dependente> BuscaId(Guid id)
        {
            return await _dependentesRepositoryRead.ObterId(id);
        }


        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("CPF do funcionário é inválido.");
            }

            var funcionario = await _dependentesRepositoryRead.BuscarPorCPF(cpf);

            if (funcionario == null)
            {
                return NotFound("Funcionário não encontrado.");
            }

            return Ok(funcionario);
        }


    }
}
