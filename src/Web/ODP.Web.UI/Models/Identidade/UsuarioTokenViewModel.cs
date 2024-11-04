using System.Collections.Generic;

namespace ODP.Web.UI.Models.Identidade
{
    public class UsuarioTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaimViewModel> Claims { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsLockedOut { get; set; }
    }
}
