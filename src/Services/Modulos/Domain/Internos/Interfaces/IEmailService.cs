using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Internos.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(List<string> destinatarios, string assunto, string mensagem);
    }


}
