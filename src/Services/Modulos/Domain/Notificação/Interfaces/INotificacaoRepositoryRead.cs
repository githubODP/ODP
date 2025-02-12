using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Notificação.Interfaces
{
    public interface INotificacaoRepositoryRead
    {
        public Task EnviarEmailNotificacao(string assunto, string corpo, string destinatarios);
    }
}
