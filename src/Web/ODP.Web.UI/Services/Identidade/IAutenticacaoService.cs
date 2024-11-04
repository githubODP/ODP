using ODP.Web.UI.Models.Identidade;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Identidade
{
    public interface IAutenticacaoService
    {
        Task<UsuarioRespostaLoginViewModel> Login(UsuarioLoginViewModel usuarioLogin);

        Task<UsuarioRespostaLoginViewModel> Registro(UsuarioRegistroViewModel usuarioRegistro);

        Task<IEnumerable<UsuarioTokenViewModel>> ListarUsuarios();
        Task<bool> DesabilitarUsuario(string userId);
        Task<bool> HabilitarUsuario(string userId);
        Task<ResultadoRedefinicaoSenhaViewModel> RedefinirSenha(RedefinirSenhaViewModel redefinirSenhaModel);
        Task<List<string>> ListarUsuariosLogados();


    }
}
