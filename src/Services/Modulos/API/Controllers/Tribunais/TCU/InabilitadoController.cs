using Domain.Tribunal.Entidades.TCU;
using Domain.Tribunal.Interfaces.TCU;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Tribunais.TCU

{
    [ApiController]
    [Route("api/inabilitado")]
    [Authorize]
    public class InabilitadoController : Controller
    {
        private readonly IInabilitadoRepositoryRead _inabilitadoRepositoryread;

        public InabilitadoController(IInabilitadoRepositoryRead inabilitadoRepository)
        {
            _inabilitadoRepositoryread = inabilitadoRepository;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _inabilitadoRepositoryread.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<Inabilitado> BuscaId(Guid id)
        {
            return await _inabilitadoRepositoryread.ObterId(id);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarCPF(string cpf)
        {
            var inabilitado = await _inabilitadoRepositoryread.BuscarCPF(cpf);

            if (inabilitado == null || !inabilitado.Any())
            {
                return NotFound("Nenhum inabilitado encontrado para o CPF fornecido.");
            }

            return Ok(inabilitado);
        }



    }
}
