
using Domain.Compras.Entidades;
using Domain.Compras.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Compras.API.Controllers
{
    [ApiController]
    [Route("api/dispensa")]
    [Authorize]
    public class DispensaController : Controller
    {
        private readonly IDispensaRepositoryRead _dispensaRepositoryRead;


        public DispensaController(IDispensaRepositoryRead dispensaRepositoryRead)

        {
            _dispensaRepositoryRead = dispensaRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _dispensaRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar/{id}")]
        public async Task<Dispensa> BuscaId(Guid id)
        {
            return await _dispensaRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarPorCNPJ(string cnpj)
        {
            var dispensa = await _dispensaRepositoryRead.BuscarCNPJ(cnpj);

            if (dispensa == null || !dispensa.Any())
            {
                return NotFound("Nenhum contrato encontrado para o CNPJ fornecido.");
            }

            return Ok(dispensa);
        }


    }
}
