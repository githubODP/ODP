
using Domain.Compras.Entidades;
using Domain.Compras.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Compras.API.Controllers
{
    [ApiController]
    [Route("api/licitacao")]
    [Authorize]
    public class LicitacaoController : Controller
    {
        private readonly ILicitacaoRepositoryRead _licitacaoRepositoryRead;

        public LicitacaoController(ILicitacaoRepositoryRead licitacaoRepositoryRead)

        {
            _licitacaoRepositoryRead = licitacaoRepositoryRead;

        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _licitacaoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("listar/{id}")]
        public async Task<Licitacao> BuscaId(Guid id)
        {
            return await _licitacaoRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarPorCNPJ(string cnpj)
        {
            var licitacao = await _licitacaoRepositoryRead.BuscarCNPJ(cnpj);

            if (licitacao == null || !licitacao.Any())
            {
                return NotFound("Nenhum contrato encontrado para o CNPJ fornecido.");
            }

            return Ok(licitacao);
        }



    }
}
