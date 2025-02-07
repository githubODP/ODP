using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.Cooperacao
{
    [ApiController]
    [Route("api/termocooperacao")]
    [Authorize]
    public class CooperacaoController : Controller
    {
        private readonly ITermoCooperacaoRepositoryRead _termoRepositoriyRead;
        private readonly ITermoCooperacaoRepository _termoRepository;

        public CooperacaoController(ITermoCooperacaoRepositoryRead termoRepositoriyRead, ITermoCooperacaoRepository termoCooperacaoRepository)
        {
            _termoRepositoriyRead = termoRepositoriyRead;
            _termoRepository = termoCooperacaoRepository;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var pagedResult = await _termoRepositoriyRead.ListarComFiltrosAsync(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpPost("adicionar")]
        public async Task<IActionResult> adicionar(TermoCooperacao termo)
        {
            try
            {
                await _termoRepository.Adicionar(termo);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost("alterar")]
        public async Task<IActionResult> Alterar(TermoCooperacao termo)
        {
            try
            {
                await _termoRepository.Atualizar(termo);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpDelete("excluir")]
        public async Task<IActionResult> excluir(TermoCooperacao termo)
        {
            try
            {
                await _termoRepository.Deletar(termo);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }


        [HttpGet("pesquisar/{protocolo}")]

        public async Task<IActionResult> ObterProtocolo([FromRoute] string protocolo)
        {
            {
                return Ok(await _termoRepositoriyRead.ObterProtocolo(protocolo));
            }
        }
    }
}
