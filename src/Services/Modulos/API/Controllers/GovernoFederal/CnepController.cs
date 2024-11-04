using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers.GovernoFederal
{
    [ApiController]
    [Route("api/cnep")]
    [Authorize]
    public class CnepController : Controller
    {
        private readonly ICnepRepositoryRead _cnepRepositoryRead;


        public CnepController(ICnepRepositoryRead cnepRepositoryRead)
        {
            _cnepRepositoryRead = cnepRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _cnepRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar/{id}")]
        public async Task<Cnep> BuscaId(Guid id)
        {
            return await _cnepRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarPorCNPJ(string cnpj)
        {
            var cnep = await _cnepRepositoryRead.BuscarCNPJ(cnpj);

            if (cnep == null || !cnep.Any())
            {
                return NotFound("Nenhuma aeronave encontrado para o CNPJ fornecido.");
            }

            return Ok(cnep);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var cnep = await _cnepRepositoryRead.BuscarCPF(cpf);

            if (cnep == null || !cnep.Any())
            {
                return NotFound("Nenhuma aeronave encontrado para o CPF fornecido.");
            }

            return Ok(cnep);
        }
    }
}
