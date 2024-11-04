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
    [Route("api/cnpjrestrito")]
    [Authorize]
    public class CNPJRestritoController : Controller
    {

        private readonly ICNPJRestritoRepositoryRead _cnpjrestritoRepository;


        public CNPJRestritoController(ICNPJRestritoRepositoryRead cnpjrestritoRepository)
        {
            _cnpjrestritoRepository = cnpjrestritoRepository;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _cnpjrestritoRepository.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<CNPJRestrito> BuscaId(Guid id)
        {
            return await _cnpjrestritoRepository.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> SearchCNPJ(string cnpj)
        {
            var CNPJ = await _cnpjrestritoRepository.BuscarCNPJ(cnpj);

            if (CNPJ == null || !CNPJ.Any())
            {
                return NotFound("Nenhuma restrição encontrada para o CNPJ fornecido.");
            }

            return Ok(CNPJ);
        }



    }
}
