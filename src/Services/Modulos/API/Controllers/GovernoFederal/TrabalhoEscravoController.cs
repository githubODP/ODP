using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.GovernoFederal.Controllers
{
    [ApiController]
    [Route("api/trabalhoescravo")]
    [Authorize]
    public class TrabalhoEscravoController : Controller
    {
        private readonly ITrabalhoEscravoRepositoryRead _trabalhoRepositoryRead;


        public TrabalhoEscravoController(ITrabalhoEscravoRepositoryRead trabalhoRepositoryRead)
        {
            _trabalhoRepositoryRead = trabalhoRepositoryRead;

        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _trabalhoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar/{id}")]
        public async Task<TrabalhoEscravo> BuscaId(Guid id)
        {
            return await _trabalhoRepositoryRead.ObterId(id);
        }


        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarPorCNPJ(string cnpj)
        {
            var trabalho = await _trabalhoRepositoryRead.BuscarCNPJ(cnpj);

            if (trabalho == null || !trabalho.Any())
            {
                return NotFound("Nenhum contrato encontrado para o CNPJ fornecido.");
            }

            return Ok(trabalho);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var trabalho = await _trabalhoRepositoryRead.BuscarCPF(cpf);

            if (trabalho == null || !trabalho.Any())
            {
                return NotFound("Nenhum contrato encontrado para o CPF fornecido.");
            }

            return Ok(trabalho);
        }


    }
}
