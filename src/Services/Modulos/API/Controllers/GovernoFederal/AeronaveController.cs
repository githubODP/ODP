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
    [Route("api/aeronave")]
    [Authorize]
    public class AeronaveController : Controller
    {
        private readonly IAeronaveRepositoryRead _aeronaveRepositoryRead;


        public AeronaveController(IAeronaveRepositoryRead aeronaveRepositoryRead)
        {
            _aeronaveRepositoryRead = aeronaveRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _aeronaveRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar/{id}")]
        public async Task<Aeronave> BuscaId(Guid id)
        {
            return await _aeronaveRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarPorCNPJ(string cnpj)
        {
            var aeronave = await _aeronaveRepositoryRead.BuscarCNPJ(cnpj);

            if (aeronave == null || !aeronave.Any())
            {
                return NotFound("Nenhuma aeronave encontrado para o CNPJ fornecido.");
            }

            return Ok(aeronave);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var aeronave = await _aeronaveRepositoryRead.BuscarCPF(cpf);

            if (aeronave == null || !aeronave.Any())
            {
                return NotFound("Nenhuma aeronave encontrado para o CPF fornecido.");
            }

            return Ok(aeronave);
        }

    }
}
