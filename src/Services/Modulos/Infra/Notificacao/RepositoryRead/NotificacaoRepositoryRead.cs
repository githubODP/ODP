using Domain.Notificação.Interfaces;
using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Notificacao.RepositoryRead
{
    public class NotificacaoRepositoryRead : INotificacaoRepositoryRead
    {
        private readonly IFluentEmail _fluentEmail;

        public NotificacaoRepositoryRead(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task EnviarEmailNotificacao(string assunto, string corpo, string destinatarios)
        {
            await _fluentEmail
                .To(destinatarios)
                .Subject(assunto)
                .Body(corpo, isHtml: true)
                .SendAsync();
        }
    }
}
