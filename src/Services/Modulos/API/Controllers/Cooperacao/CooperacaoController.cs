using Domain.Internos.Entidade;
using Domain.Internos.Enum;
using Domain.Internos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

                // Busca o objeto atualizado no banco de dados
                var termoAtualizado = await _termoRepositoriyRead.ObterId(termo.Id);

                return Ok(termoAtualizado); // Retorna o objeto atualizado
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("excluir")]
        public async Task<IActionResult> Excluir([FromBody] TermoCooperacao termo)
        {
            if (termo == null)
                return BadRequest("O termo não pode ser nulo.");

            try
            {
                await _termoRepository.Deletar(termo);

                // Retorna o próprio termo excluído
                return Ok(termo);
            }
            catch (Exception ex)
            {
                // Caso ocorra erro, retorna um termo padrão
                var termoPadrao = new TermoCooperacao
                {
                    Id = Guid.NewGuid(),
                    Protocolo = "ERRO",
                    Orgao = "Erro ao excluir",
                    Sigla = "N/A",
                    NroTermo = "0000",
                    InicioVigencia = DateTime.MinValue,
                    FimVIgencia = DateTime.MinValue,
                    Validade = 0,
                    Ativo = false,
                    Status = ETipoStatus.SELECIONE, 
                    Renovar = false,
                    DIOE = 0,
                    DataPublicacao = DateTime.MinValue,
                    Objeto = "Erro",
                    Regulamentacao = "N/A",
                    Informacoes = "Erro ao excluir",
                    Observacao = "Erro ao excluir"
                };

                return StatusCode(500, termoPadrao);
            }
        }




        [HttpGet("pesquisar/{protocolo}")]

        public async Task<IActionResult> ObterProtocolo([FromRoute] string protocolo)
        {
            {
                return Ok(await _termoRepositoriyRead.ObterProtocolo(protocolo));
            }
        }

        [HttpGet("obterid/{id}")]
        public async Task<IActionResult> ObterID( Guid id)
        {
            return Ok (await _termoRepositoriyRead.ObterId(id));
        }


      
    }
}
