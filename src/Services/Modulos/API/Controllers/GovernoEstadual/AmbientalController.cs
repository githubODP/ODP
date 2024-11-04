using Domain.GovernoEstadual.Entidades;
using Domain.GovernoEstadual.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.GovernoEstadual
{

    [ApiController]
    [Route("api/ambiental")]
    [Authorize]
    public class AmbientalController : Controller
    {
        private readonly IAmbientalRepositoryRead _ambientalRepositoryRead;

        public AmbientalController(IAmbientalRepositoryRead ambientalRepositoryRead)
        {
            _ambientalRepositoryRead = ambientalRepositoryRead;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _ambientalRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<Ambiental> BuscaId(Guid id)
        {
            return await _ambientalRepositoryRead.ObterId(id);
        }
        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> SearchCNPJ(string cnpj)
        {
            var ambiental = await _ambientalRepositoryRead.BuscarCNPJ(cnpj);

            if (ambiental == null || !ambiental.Any())
            {
                return NotFound("Nenhuma multa  encontrado para o CNPJ fornecido.");
            }

            return Ok(ambiental);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            var ambiental = await _ambientalRepositoryRead.BuscarCPF(cpf);

            if (ambiental == null || !ambiental.Any())
            {
                return NotFound("Nenhuma multa  encontrado para o CPF fornecido.");
            }

            return Ok(ambiental);
        }




    }
}
