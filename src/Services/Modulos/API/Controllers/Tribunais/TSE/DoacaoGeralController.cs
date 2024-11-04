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
    [Route("api/doacaogeral")]
    [Authorize]
    public class DoacaoGeralController : Controller
    {
        private readonly IDoacaoGeralRepositoryRead _doacaoGeralRepositoryRead;

        public DoacaoGeralController(IDoacaoGeralRepositoryRead doacaoGeralRepositoryRead)
        {
            _doacaoGeralRepositoryRead = doacaoGeralRepositoryRead;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _doacaoGeralRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<DoacaoGeral> BuscaId(Guid id)
        {
            return await _doacaoGeralRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarCPF(string cpf)
        {
            var doacoes = await _doacaoGeralRepositoryRead.BuscarCPF(cpf);

            if (doacoes == null || !doacoes.Any())
            {
                return NotFound("Nenhuma doação encontrada para o CPF fornecido.");
            }

            return Ok(doacoes);


        }
    }
}
