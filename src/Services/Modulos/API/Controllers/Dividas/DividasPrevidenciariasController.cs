
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
    [Route("api/dividaprevidenciarias")]
    [Authorize]
    public class DividasPrevidenciariasController : Controller
    {


        private readonly IDividaPrevidenciaRepositoryRead _dividaPrevidenciaRepositoryread;


        public DividasPrevidenciariasController(IDividaPrevidenciaRepositoryRead dividasPrevidenciaRepository)
        {

            _dividaPrevidenciaRepositoryread = dividasPrevidenciaRepository;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _dividaPrevidenciaRepositoryread.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consulta/{id}")]
        public async Task<DividaPrevidenciaria> BuscaId(Guid id)
        {
            return await _dividaPrevidenciaRepositoryread.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> SearchCNPJ(string cnpj)
        {
            var dividas = await _dividaPrevidenciaRepositoryread.BuscarCNPJ(cnpj);

            if (dividas == null || !dividas.Any())
            {
                return NotFound("Nenhuma divida  encontrado para o CNPJ fornecido.");
            }

            return Ok(dividas);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            var dividas = await _dividaPrevidenciaRepositoryread.BuscarCNPJ(cpf);

            if (dividas == null || !dividas.Any())
            {
                return NotFound("Nenhuma divida  encontrado para o CPF fornecido.");
            }

            return Ok(dividas);
        }


    }
}
