using System.ComponentModel.DataAnnotations;

namespace ODP.Web.UI.Models.Identidade
{
    public class RedefinirSenhaViewModel
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string NovaSenha { get; set; }

        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem.")]
        public string NovaSenhaConfirmacao { get; set; }
    }
}
