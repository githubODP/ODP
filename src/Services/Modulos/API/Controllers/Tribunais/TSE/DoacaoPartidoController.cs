using Domain.Tribunal.Entidades.TSE;
using Domain.Tribunal.Interfaces.TSE;
using Infra.Tribunal.Repository.TSE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Tribunais.TSE
{
    [ApiController]
    [Route("api/doacaopartido")]
    [Authorize]
    public class DoacaoPartidoController : Controller
    {
        private readonly IDoacaoPartidoRepositoryRead _doacaoPartidoRepositoryRead;

        public DoacaoPartidoController(IDoacaoPartidoRepositoryRead doacaopartidoRepositoryRead)
        {
            _doacaoPartidoRepositoryRead = doacaopartidoRepositoryRead;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _doacaoPartidoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<DoacaoPartido> BuscaId(Guid id)
        {
            return await _doacaoPartidoRepositoryRead.ObterId(id);
        }
        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarCNPJ(string cnpj)
        {
            var doacoes = await _doacaoPartidoRepositoryRead.BuscarCNPJ(cnpj);

            if (doacoes == null || !doacoes.Any())
            {
                return NotFound("Nenhuma Doação encontrado para o CNPJ fornecido.");
            }

            return Ok(doacoes);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarCPF(string cpf)
        {
            var doacoes = await _doacaoPartidoRepositoryRead.BuscarCPF(cpf);

            if (doacoes == null || !doacoes.Any())
            {
                return NotFound("Nenhuma Doação encontrado para o CPF fornecido.");
            }

            return Ok(doacoes);
        }
    }
}
