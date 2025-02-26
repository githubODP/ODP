using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Infra.Internos.Repositories
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        private void ConfigurarProxy()
        {
            WebProxy proxy = new WebProxy(_emailSettings.ProxyUrl, _emailSettings.ProxyPort)
            {
                Credentials = new NetworkCredential(_emailSettings.ProxyUsuario, _emailSettings.ProxySenha),
                BypassProxyOnLocal = false
            };

            WebRequest.DefaultWebProxy = proxy;
        }

        public async Task EnviarEmailAsync(List<string> destinatarios, string assunto, string mensagem)
        {
            try
            {
                ConfigurarProxy();

                using (var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_emailSettings.EmailRemetente, _emailSettings.Senha);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Timeout = 20000;

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_emailSettings.EmailRemetente, "xxxorgao - Notificação");
                        mailMessage.Subject = assunto;
                        mailMessage.Body = mensagem;
                        mailMessage.IsBodyHtml = true;

                        foreach (var destinatario in destinatarios)
                        {
                            mailMessage.To.Add(destinatario);
                        }

                        await client.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro SMTP ao enviar e-mail: {ex.Message}", ex);
            }
        }
    }
}
