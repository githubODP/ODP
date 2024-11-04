using Domain.GovernoFederal.Entidades;
using ODP.Web.UI.Models.ViewModels.GovernoFederal;

namespace ODP.Web.UI.Models.Interfaces.GovernoFederal
{
    public interface ICepimService : IRepositoryService<CepimViewModel>, IBuscarDados<CepimViewModel>
    {
    }
}
