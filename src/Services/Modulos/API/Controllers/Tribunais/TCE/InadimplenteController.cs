using Domain.Tribunal.Entidades.TCE;
using Domain.Tribunal.Interfaces.TCE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Tribunais.TCE
{
    [ApiController]
    [Route("api/inadimplente")]
    [Authorize]
    public class InadimplenteController : Controller
    {
        private readonly IInadimplenteRepositoryRead _inadimplenteRepositoryread;


        public InadimplenteController(IInadimplenteRepositoryRead inadimplenteRepository)
        {
            _inadimplenteRepositoryread = inadimplenteRepository;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _inadimplenteRepositoryread.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<Inadimplente> BuscaId(Guid id)
        {
            return await _inadimplenteRepositoryread.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> SearchCNPJ(string cnpj)
        {
            var inadimplentes = await _inadimplenteRepositoryread.BuscarCNPJ(cnpj);

            if (inadimplentes == null || !inadimplentes.Any())
            {
                return NotFound("Nenhuma Inadimplencia encontrado para o CNPJ fornecido.");
            }

            return Ok(inadimplentes);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            var inadimplentes = await _inadimplenteRepositoryread.BuscarCPF(cpf);

            if (inadimplentes == null || !inadimplentes.Any())
            {
                return NotFound("Nenhuma Inadimplencia encontrado para o CPF fornecido.");
            }

            return Ok(inadimplentes);
        }


    }
}
