
using Dividas.Domain.Entidades;
using Dividas.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dividas.API.Controllers
{
    [ApiController]
    [Route("api/dividanaoprevidenciarias")]
    [Authorize]
    public class DividasNaoPrevidenciariasController : Controller
    {


        private readonly IDividaNaoPrevidenciaRepositoryRead _naoPrevidenciaRepositoryRead;



        public DividasNaoPrevidenciariasController(IDividaNaoPrevidenciaRepositoryRead naoPrevidenciaRepositoryRead)
        {
            _naoPrevidenciaRepositoryRead = naoPrevidenciaRepositoryRead;
        }



        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _naoPrevidenciaRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consulta/{id}")]
        public async Task<DividaNaoPrevidenciaria> BuscaId(Guid id)
        {
            return await _naoPrevidenciaRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> SearchCNPJ(string cnpj)
        {
            var dividas = await _naoPrevidenciaRepositoryRead.BuscarCNPJ(cnpj);

            if (dividas == null || !dividas.Any())
            {
                return NotFound("Nenhuma divida  encontrado para o CNPJ fornecido.");
            }

            return Ok(dividas);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            var dividas = await _naoPrevidenciaRepositoryRead.BuscarCNPJ(cpf);

            if (dividas == null || !dividas.Any())
            {
                return NotFound("Nenhuma divida  encontrado para o CPF fornecido.");
            }

            return Ok(dividas);
        }


    }
}
