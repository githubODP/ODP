using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace API.Controllers.Cooperacao
{
    [ApiController]
    [Route("api/termocooperacao")]
    [Authorize]

    public class CooperacaoController : Controller
    {
        private readonly ITermoCooperacaoRepositoryRead _termoRepositoryRead;
        private readonly ITermoCooperacaoRepository _termoRepository;
        private readonly IEmailService _emailService;
        private readonly EmailSettings _emailSettings;

        public CooperacaoController(ITermoCooperacaoRepositoryRead termoRepositoriyRead,
                                    ITermoCooperacaoRepository termoCooperacaoRepository,
                                    IEmailService emailService,
                                    IOptions<EmailSettings> emailSettings)
        {
            _termoRepositoryRead = termoRepositoriyRead;
            _termoRepository = termoCooperacaoRepository;
            _emailService = emailService;
            _emailSettings = emailSettings.Value;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string termo =null)
        {
            var pagedResult = await _termoRepositoryRead.Listar(pageNumber, pageSize, termo);
            return Ok(pagedResult);
        }


        [HttpGet("obterid/{id}")]
        public async Task<IActionResult> ObterID(Guid id)
        {
            return Ok(await _termoRepositoryRead.ObterId(id));
        }

       

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] TermoCooperacao termo)
        {
            if (termo == null)
                return BadRequest("Dados inválidos.");

            await _termoRepository.Adicionar(termo);
            return Ok(termo);
        }


        [HttpPut("alterar/{id}")]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] TermoCooperacao termo)
        {
            if (termo == null || id == Guid.Empty)
                return BadRequest("Dados inválidos.");

            var termoExistente = await _termoRepositoryRead.ObterId(id);
            if (termoExistente == null)
                return NotFound("Termo de cooperação não encontrado.");
            
            termo.Id = id;
            await _termoRepository.Atualizar(termo);

            return Ok(termo);
        }

        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var termoExistente = await _termoRepositoryRead.ObterId(id);
            if (termoExistente == null)
                return NotFound("Termo de cooperação não encontrado.");

            await _termoRepository.Deletar(termoExistente);
            return Ok("Termo excluído com sucesso.");
        }



        [HttpGet("listar-envio")]
        public async Task<ActionResult<List<TermoCooperacao>>> ListarEnvio()
        {
            var termos = await _termoRepositoryRead.ListarEnvio(); // Obtém os dados filtrados do repositório

            if (termos == null || !termos.Any())
            {
                return Ok(new List<TermoCooperacao>()); // Retorna lista vazia caso não haja alertas
            }

            // Converte para ViewModel se necessário
            var resultado = termos.Select(t => new TermoCooperacao
            {
                Id = t.Id,
                Protocolo = t.Protocolo,
                NroTermo = t.NroTermo,
                InicioVigencia = t.InicioVigencia,
                FimVigencia = t.FimVigencia,

            }).ToList();

            return Ok(resultado);
        }

        [HttpPost("enviar-alertas")]
        public async Task<IActionResult> EnviarAlertasPorEmail()
        {
            try
            {
                // Obtém as mensagens dos alertas a serem enviados
                var mensagens = await _termoRepositoryRead.EnviarAlertasPorEmail();

                if (!mensagens.Any())
                    return Ok("Nenhum termo para envio de alerta.");

                // Lista de destinatários dinâmica (configurada no appsettings.json)
                var destinatarios = _emailSettings.ToEmails;

                if (!destinatarios.Any())
                    return BadRequest("Nenhum destinatário configurado para envio de e-mails.");

                // Concatena todas as mensagens em um único corpo de e-mail
                var corpoEmail = string.Join(Environment.NewLine, mensagens);
                string assunto = "Alertas de Vencimento - Termos de Cooperação";

                // Envia o e-mail para todos os destinatários
                await _emailService.EnviarEmailAsync(destinatarios, assunto, corpoEmail);

                return Ok($"E-mail enviado com sucesso para {destinatarios.Count} destinatários.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao enviar e-mails: {ex.Message}");
            }
        }

    }
}
