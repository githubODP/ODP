using Domain.Tribunal.Entidades.TSE;
using Domain.Tribunal.Interfaces.TSE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Tribunais.TSE
{
    [ApiController]
    [Route("api/doacaocandidato")]
    [Authorize]
    public class DoacaoCandidatoController : Controller
    {
        private readonly IDoacaoCandidatoRepositoryRead _doacaoCandidatoRepositoryRead;

        public DoacaoCandidatoController(IDoacaoCandidatoRepositoryRead doacaoCandidatoRepositoryRead)
        {
            _doacaoCandidatoRepositoryRead = doacaoCandidatoRepositoryRead;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _doacaoCandidatoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<DoacaoCandidato> BuscaId(Guid id)
        {
            return await _doacaoCandidatoRepositoryRead.ObterId(id);
        }


        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarCNPJ(string cnpj)
        {
            var doacoes = await _doacaoCandidatoRepositoryRead.BuscarCNPJ(cnpj);

            if (doacoes == null || !doacoes.Any())
            {
                return NotFound("Nenhuma Doação encontrado para o CNPJ fornecido.");
            }

            return Ok(doacoes);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarCPF(string cpf)
        {
            var doacoes = await _doacaoCandidatoRepositoryRead.BuscarCPF(cpf);

            if (doacoes == null || !doacoes.Any())
            {
                return NotFound("Nenhuma Doação encontrado para o CPF fornecido.");
            }

            return Ok(doacoes);
        }


    }
}
