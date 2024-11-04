using Domain.Fazenda.Entidades;
using Domain.Fazenda.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fazenda.API.Controllers
{

    [ApiController]
    [Route("api/jucepar")]
    [Authorize]
    public class JuceparController : Controller
    {

        private readonly IJuceparRepositoryRead _juceparRepositoryRead;




        public JuceparController(IJuceparRepositoryRead juceparRepositoryRead)

        {
            _juceparRepositoryRead = juceparRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _juceparRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consulta/{id}")]
        public async Task<Jucepar> BuscaId(Guid id)
        {
            return await _juceparRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> SearchCNPJ(string cnpj)
        {
            var junta = await _juceparRepositoryRead.BuscarCNPJ(cnpj);

            if (junta == null || !junta.Any())
            {
                return NotFound("Nenhuma divida  encontrado para o CNPJ fornecido.");
            }

            return Ok(junta);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            var junta = await _juceparRepositoryRead.BuscarCPF(cpf);

            if (junta == null || !junta.Any())
            {
                return NotFound("Nenhuma divida  encontrado para o CNPJ fornecido.");
            }

            return Ok(junta);
        }





    }

}
