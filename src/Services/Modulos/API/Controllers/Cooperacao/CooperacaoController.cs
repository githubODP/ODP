using CGEODP.WebApi.Core.Identidade;
using Domain.Internos.Entidade;
using Domain.Internos.Enum;
using Domain.Internos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public CooperacaoController(ITermoCooperacaoRepositoryRead termoRepositoriyRead, ITermoCooperacaoRepository termoCooperacaoRepository, IEmailService emailService)
        {
            _termoRepositoryRead = termoRepositoriyRead;
            _termoRepository = termoCooperacaoRepository;
            _emailService = emailService;
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

        [ClaimsAuthorize("","")]

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
            // Obtém as mensagens dos alertas a serem enviados
            var mensagens = await _termoRepositoryRead.EnviarAlertasPorEmail();

            if (!mensagens.Any())
                return Ok("Nenhum termo para envio de alerta.");

            // Lista de destinatários dinâmica
            var destinatarios = new List<string>
    {
                "barbalho@cge.pr.gov.br",
                "eduardojr@cge.pr.gov.br",
                "maria.oliveira@cge.pr.gov.br"
    };

            // Dispara os e-mails com cada mensagem gerada
            foreach (var mensagem in mensagens)
            {
                string assunto = "Alerta de Vencimento - Termo de Cooperação";
                await _emailService.EnviarEmailAsync(destinatarios, assunto, mensagem);
            }

            return Ok("E-mails enviados com sucesso.");
        }


        //[HttpPost("enviar-alerta")]
        //public async Task<IActionResult> EnviarAlertaPorEmail()
        //{
        //    var termos = await _termoRepositoryRead.ListarEnvio(); // Obtém os registros filtrados

        //    if (termos == null || !termos.Any())
        //    {
        //        return Ok("Nenhum termo se encaixa nos critérios de alerta.");
        //    }

        //    // Define os destinatários fixos
        //    List<string> destinatarios = new List<string>
        //    {
        //        "eduardojr@cge.pr.gov.br",
        //        "rivaltersilva@gmail.com"
        //    };

        //    // Monta o conteúdo do e-mail
        //    string assunto = "🚨 Alertas de Termos de Cooperação Vencendo";
        //    StringBuilder mensagem = new StringBuilder();
        //    mensagem.AppendLine("<h2>Lista de Termos de Cooperação Próximos do Fim</h2>");
        //    mensagem.AppendLine("<table border='1' style='border-collapse: collapse; width: 100%;'>");
        //    mensagem.AppendLine("<tr><th>Protocolo</th><th>Nº Termo</th><th>Início Vigência</th><th>Fim Vigência</th><th>Dias Restantes</th></tr>");

        //    foreach (var termo in termos)
        //    {
        //        int diasRestantes = (termo.FimVigencia - DateTime.UtcNow.Date).Days;
        //        mensagem.AppendLine($"<tr><td>{termo.Protocolo}</td><td>{termo.NroTermo}</td><td>{termo.InicioVigencia:dd/MM/yyyy}</td><td>{termo.FimVigencia:dd/MM/yyyy}</td><td>{diasRestantes}</td></tr>");
        //    }

        //    mensagem.AppendLine("</table>");
        //    mensagem.AppendLine("<p>Atenciosamente,<br>Sistema de Cooperação</p>");

        //    // Enviar o e-mail
        //    try
        //    {
        //        await EnviarEmail(destinatarios, assunto, mensagem.ToString());
        //        return Ok("✅ E-mail enviado com sucesso!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"❌ Erro ao enviar e-mail: {ex.Message}");
        //    }
        //}


        //private async Task EnviarEmail(List<string> destinatarios, string assunto, string mensagem)
        //{
        //    try
        //    {
        //        Console.WriteLine("⏳ Iniciando envio de e-mail...");

        //        using (var smtpClient = new SmtpClient("expressomx.pr.gov.br"))
        //        {
        //            smtpClient.Port = 993;
        //            smtpClient.Credentials = new NetworkCredential("rivaltersilva@expresso.pr.gov.br", "GueEdu2350");
        //            smtpClient.EnableSsl = true;

        //            var mailMessage = new MailMessage
        //            {
        //                From = new MailAddress("rivaltersilva@expresso.pr.gov.br"),
        //                Subject = assunto,
        //                Body = mensagem,
        //                IsBodyHtml = true
        //            };

        //            foreach (var destinatario in destinatarios)
        //            {
        //                mailMessage.To.Add(destinatario);
        //            }

        //            Console.WriteLine("📨 Tentando enviar e-mail...");
        //            await smtpClient.SendMailAsync(mailMessage); // 🔴 Verificar se trava aqui
        //            Console.WriteLine("✅ E-mail enviado com sucesso!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"❌ Erro ao enviar e-mail: {ex.Message}");
        //    }
        //}



    }
}
