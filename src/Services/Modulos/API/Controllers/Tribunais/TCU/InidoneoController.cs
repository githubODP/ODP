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
    [Route("api/inidoneo")]
    [Authorize]

    public class InidoneoController : Controller
    {
        private readonly IInidoneoRepositoryRead _inidoneoRepository;

        public InidoneoController(IInidoneoRepositoryRead inidoneoRepository)
        {
            _inidoneoRepository = inidoneoRepository;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _inidoneoRepository.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("consultaid/{id}")]
        public async Task<Inidoneo> BuscaId(Guid id)
        {
            return await _inidoneoRepository.ObterId(id);
        }




        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarCNPJ(string cnpj)
        {
            var inidoneos = await _inidoneoRepository.BuscarCNPJ(cnpj);

            if (inidoneos == null || !inidoneos.Any())
            {
                return NotFound("Nenhuma Ocorrência encontrado para o CNPJ fornecido.");
            }

            return Ok(inidoneos);
        }


    }
}
