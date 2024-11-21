using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.GovernoFederal
{
    [ApiController]
    [Route("api/cepim")]
    [Authorize]
    public class CepimController : Controller
    {
        private readonly ICepimRepositoryRead _cepimRepositoryRead;


        public CepimController(ICepimRepositoryRead cepimRepositoryRead)
        {
            _cepimRepositoryRead = cepimRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _cepimRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar/{id}")]
        public async Task<Cepim> BuscaId(Guid id)
        {
            return await _cepimRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarPorCNPJ(string cnpj)
        {
            var cepim = await _cepimRepositoryRead.BuscarCNPJ(cnpj);

            if (cepim == null || !cepim.Any())
            {
                return NotFound("Nenhuma aeronave encontrado para o CNPJ fornecido.");
            }

            return Ok(cepim);
        }


    }
}
