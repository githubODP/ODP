namespace Domain.Internos.Interfaces
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(List<string> destinatarios, string assunto, string mensagem);
    }


}
