using ODP.Web.UI.Models.ViewModels.GovernoEstadual;

namespace ODP.Web.UI.Models.Interfaces.GovernoEstadual
{
    public interface IAmbientalService : IRepositoryService<AmbientalViewModel>,
                                        IBuscarDados<AmbientalViewModel>
    {
    }
}
