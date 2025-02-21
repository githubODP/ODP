using Domain.Internos.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Infra.Internos.Repositories
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp-mail.outlook.com";
        private readonly int _smtpPort = 587;
        private readonly string _emailRemetente = "valterssilva@hotmail.com";
        private readonly string _senha = "xxxxxx";


        private void ConfigurarProxy()
        {
            WebProxy proxy = new WebProxy("proxy01.ouvidoria.parana", 8080)
            {
                Credentials = new NetworkCredential("rivaltersilva", "GueEdu2350"),
                BypassProxyOnLocal = false
            };


            WebRequest.DefaultWebProxy = proxy;
        }



        public async Task EnviarEmailAsync(List<string> destinatarios, string assunto, string mensagem)
        {
            try
            {
                // Configurar Proxy antes da requisição SMTP
                ConfigurarProxy();

                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_emailRemetente, _senha);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Timeout = 20000;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailRemetente, "CGÉ - Notificação"),
                        Subject = assunto,
                        Body = mensagem,
                        IsBodyHtml = true
                    };

                    // Adiciona os destinatários
                    foreach (var destinatario in destinatarios)
                    {
                        mailMessage.To.Add(destinatario);
                    }

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro SMTP ao enviar e-mail: {ex.Message}");
            }
        }

    }
}
