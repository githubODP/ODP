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
    [Route("api/bolsa")]
    [Authorize]
    public class BolsaFamiliaController : Controller
    {
        private readonly IBolsaFamiliaRepositoryRead _bolsaRepositoryRead;


        public BolsaFamiliaController(IBolsaFamiliaRepositoryRead bolsaRepositoryRead)
        {
            _bolsaRepositoryRead = bolsaRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _bolsaRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar/{id}")]
        public async Task<BolsaFamilia> BuscaId(Guid id)
        {
            return await _bolsaRepositoryRead.ObterId(id);
        }


        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var bolsa = await _bolsaRepositoryRead.BuscarCPF(cpf);

            if (bolsa == null || !bolsa.Any())
            {
                return NotFound("Nenhuma aeronave encontrado para o CPF fornecido.");
            }

            return Ok(bolsa);
        }
    }
}
