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
    [Route("api/acordo")]
    [Authorize]
    public class AcordoLenienciaController : Controller
    {
        private readonly IAcordoLenienciaRepositoryRead _acordoRepositoryRead;


        public AcordoLenienciaController(IAcordoLenienciaRepositoryRead acordoRepositoryRead)
        {
            _acordoRepositoryRead = acordoRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _acordoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar/{id}")]
        public async Task<AcordoLeniencia> BuscaId(Guid id)
        {
            return await _acordoRepositoryRead.ObterId(id);
        }



        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarPorCNPJ(string cnpj)
        {
            var acordo = await _acordoRepositoryRead.BuscarCNPJ(cnpj);

            if (acordo == null || !acordo.Any())
            {
                return NotFound("Nenhum acordo encontrado para o CNPJ fornecido.");
            }

            return Ok(acordo);
        }


    }
}
