using Domain.Internos.Entidade;
using Domain.Internos.Enum;
using Domain.Internos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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

        public CooperacaoController(ITermoCooperacaoRepositoryRead termoRepositoriyRead, ITermoCooperacaoRepository termoCooperacaoRepository)
        {
            _termoRepositoryRead = termoRepositoriyRead;
            _termoRepository = termoCooperacaoRepository;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var pagedResult = await _termoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("listar-com-filtro")]
        public async Task<IActionResult> ListarComFiltroAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string protocolo = null)
        {
            var resultado = await _termoRepositoryRead.ListarComFiltroAsync(pageNumber, pageSize, protocolo);

            if (resultado == null || !resultado.Results.Any())
            {
                return NotFound("Nenhum registro encontrado para os filtros informados.");
            }

            return Ok(resultado);
        }


        [HttpPost("adicionar")]
        public async Task<IActionResult> adicionar(TermoCooperacao termo)
        {
            try
            {
                await _termoRepository.Adicionar(termo);
                return Ok(termo);
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
                var termoAtualizado = await _termoRepositoryRead.ObterId(termo.Id);

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
                    FimVigencia = DateTime.MinValue,
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
                return Ok(await _termoRepositoryRead.ObterProtocolo(protocolo));
            }
        }

        [HttpGet("obterid/{id}")]
        public async Task<IActionResult> ObterID(Guid id)
        {
            return Ok(await _termoRepositoryRead.ObterId(id));
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


        [HttpPost("enviar-alerta")]
        public async Task<IActionResult> EnviarAlertaPorEmail()
        {
            var termos = await _termoRepositoryRead.ListarEnvio(); // Obtém os registros filtrados

            if (termos == null || !termos.Any())
            {
                return Ok("Nenhum termo se encaixa nos critérios de alerta.");
            }

            // Define os destinatários fixos
            List<string> destinatarios = new List<string>
            {
                "rivaltersilva@cge.pr.gov.br",
                "rivaltersilva@gmail.com"
            };

            // Monta o conteúdo do e-mail
            string assunto = "🚨 Alertas de Termos de Cooperação Vencendo";
            StringBuilder mensagem = new StringBuilder();
            mensagem.AppendLine("<h2>Lista de Termos de Cooperação Próximos do Fim</h2>");
            mensagem.AppendLine("<table border='1' style='border-collapse: collapse; width: 100%;'>");
            mensagem.AppendLine("<tr><th>Protocolo</th><th>Nº Termo</th><th>Início Vigência</th><th>Fim Vigência</th><th>Dias Restantes</th></tr>");

            foreach (var termo in termos)
            {
                int diasRestantes = (termo.FimVigencia - DateTime.UtcNow.Date).Days;
                mensagem.AppendLine($"<tr><td>{termo.Protocolo}</td><td>{termo.NroTermo}</td><td>{termo.InicioVigencia:dd/MM/yyyy}</td><td>{termo.FimVigencia:dd/MM/yyyy}</td><td>{diasRestantes}</td></tr>");
            }

            mensagem.AppendLine("</table>");
            mensagem.AppendLine("<p>Atenciosamente,<br>Sistema de Cooperação</p>");

            // Enviar o e-mail
            try
            {
                await EnviarEmail(destinatarios, assunto, mensagem.ToString());
                return Ok("✅ E-mail enviado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ Erro ao enviar e-mail: {ex.Message}");
            }
        }


        private async Task EnviarEmail(List<string> destinatarios, string assunto, string mensagem)
        {
            try
            {
                Console.WriteLine("⏳ Iniciando envio de e-mail...");

                using (var smtpClient = new SmtpClient("smtp-mail.outlook.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("valterssilva@hotmail.com", "xuxu@2350");
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("valterssilva@hotmail.com"),
                        Subject = assunto,
                        Body = mensagem,
                        IsBodyHtml = true
                    };

                    foreach (var destinatario in destinatarios)
                    {
                        mailMessage.To.Add(destinatario);
                    }

                    Console.WriteLine("📨 Tentando enviar e-mail...");
                    await smtpClient.SendMailAsync(mailMessage); // 🔴 Verificar se trava aqui
                    Console.WriteLine("✅ E-mail enviado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao enviar e-mail: {ex.Message}");
            }
        }



    }
}
