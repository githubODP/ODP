using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Internos.Entidade
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string EmailRemetente { get; set; }
        public string Senha { get; set; }
        public string ProxyUrl { get; set; }
        public int ProxyPort { get; set; }
        public string ProxyUsuario { get; set; }
        public string ProxySenha { get; set; }
        public List<string> ToEmails { get; set; }
    }
}
