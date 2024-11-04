

using ODP.Web.UI.Models.ViewModels.Dividas;
using ODP.Web.UI.Models.ViewModels.GovernoFederal;

namespace ODP.Web.UI.Models.Interfaces.GovernoFederal
{
    public interface IAeronaveService : IRepositoryService<AeronaveViewModel>,
                                        IBuscarDados<AeronaveViewModel>
    {


    }
}
