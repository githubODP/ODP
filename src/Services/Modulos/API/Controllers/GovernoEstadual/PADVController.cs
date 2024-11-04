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
    [Route("api/padv")]
    [Authorize]
    public class PADVController : Controller
    {

        private readonly IPADVRepositoryRead _padvRepositoryRead;

        public PADVController(IPADVRepositoryRead padvRepositoryRead)
        {
            _padvRepositoryRead = padvRepositoryRead;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _padvRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<PADV> BuscaId(Guid id)
        {
            return await _padvRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> SearchCNPJ(string cnpj)
        {
            var padv = await _padvRepositoryRead.BuscarCNPJ(cnpj);

            if (padv == null || !padv.Any())
            {
                return NotFound("Nenhuma padv  encontrado para o CNPJ fornecido.");
            }

            return Ok(padv);
        }


    }
}
