using ODP.Web.UI.Models.ViewModels.RecursosHumanos;

namespace ODP.Web.UI.Models.Interfaces.RecursosHumanos
{
    public interface IFuncionarioService : IRepositoryService<FuncionarioViewModel>, IBuscarDados<FuncionarioViewModel>
    {
    }
}
