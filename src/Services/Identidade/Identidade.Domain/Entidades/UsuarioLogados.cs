namespace Identidade.Domain.Entidades
{
    public class UsuarioLogados
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime LoggedInAt { get; set; }
    }
}
