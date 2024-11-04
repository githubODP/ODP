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
    [Route("api/cpfrestrito")]
    [Authorize]
    public class CPFRestritoController : Controller
    {
        private readonly ICPFRestritoRepositoryRead _cpfrestritoRepository;


        public CPFRestritoController(ICPFRestritoRepositoryRead cpfrestritoRepository)
        {
            _cpfrestritoRepository = cpfrestritoRepository;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _cpfrestritoRepository.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("consultaid/{id}")]
        public async Task<CPFRestrito> BuscaId(Guid id)
        {
            return await _cpfrestritoRepository.ObterId(id);
        }



        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            var cpfRestrito = await _cpfrestritoRepository.BuscarCPF(cpf);

            if (cpfRestrito == null || !cpfRestrito.Any())
            {
                return NotFound("Nenhuma restrição encontrada para o CPF fornecido.");
            }

            return Ok(cpfRestrito);
        }


    }
}
