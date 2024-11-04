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
    [Route("api/doacaopartidogeral")]
    [Authorize]
    public class DoacaoPartidoGeralController : Controller
    {

        private readonly IDoacaoPartidoGeralRepositoryRead _doacaoPartidoGeralRepositoryRead;

        public DoacaoPartidoGeralController(IDoacaoPartidoGeralRepositoryRead doacaopartidoGeralRepositoryRead)
        {
            _doacaoPartidoGeralRepositoryRead = doacaopartidoGeralRepositoryRead;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _doacaoPartidoGeralRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<DoacaoPartidoGeral> BuscaId(Guid id)
        {
            return await _doacaoPartidoGeralRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarCPF(string cpf)
        {
            var doacao = await _doacaoPartidoGeralRepositoryRead.BuscarCPF(cpf);

            if (doacao == null || !doacao.Any())
            {
                return NotFound("Nenhuma doação encontrado para o CPF fornecido.");
            }

            return Ok(doacao);
        }
    }
}
