

namespace Identidade.Domain.Entidades
{
    public class UsuarioToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaim> Claims { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
