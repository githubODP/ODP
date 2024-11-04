using Domain.Fazenda.Entidades;
using Domain.Fazenda.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Fazenda
{
    [ApiController]
    [Route("api/notasfiscaisfederais")]
    [Authorize]
    public class NFEletronicaFederalController : Controller
    {
        private readonly INFEletronicaFederalRepositoryRead _nfeRepositoryRead;


        public NFEletronicaFederalController(INFEletronicaFederalRepositoryRead nfeRepositoryRead)
        {
            _nfeRepositoryRead = nfeRepositoryRead;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _nfeRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("consulta/{id}")]
        public async Task<NFEletronicaFederal> BuscaId(Guid id)
        {
            return await _nfeRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> SearchCNPJ(string cnpj)
        {
            var nota = await _nfeRepositoryRead.BuscarCNPJ(cnpj);

            if (nota == null || !nota.Any())
            {
                return NotFound("Nenhuma nota  encontrado para o CNPJ fornecido.");
            }

            return Ok(nota);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            var nota = await _nfeRepositoryRead.BuscarCPF(cpf);

            if (nota == null || !nota.Any())
            {
                return NotFound("Nenhuma nota  encontrado para o CPF fornecido.");
            }

            return Ok(nota);
        }

    }
}
