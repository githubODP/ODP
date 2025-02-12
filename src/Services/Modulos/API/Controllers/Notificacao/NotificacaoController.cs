using Domain.Notificação.Interfaces; // Adicione este using
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Notificacao
{
    [ApiController]
    [Route("api/notificacao")]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoRepositoryRead _notificacao;

        public NotificacaoController(INotificacaoRepositoryRead notificacao)
        {
            _notificacao = notificacao;
        }

        [HttpPost("notificar")]
        public async Task<IActionResult> Notificar()
        {
            var assunto = "Notificação de Término de Vigência";
            var corpo = "<p>O termo de cooperação está próximo do vencimento. Faltam 100 dias para o término.</p>";
            var destinatarios = "rivaltersilva@cge.pr.gov.br";

            await _notificacao.EnviarEmailNotificacao(assunto, corpo, destinatarios);

            return Ok("E-mail enviado com sucesso!");
        }
    }
}