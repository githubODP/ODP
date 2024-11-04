using ODP.Web.UI.Models.ViewModels.Dividas;
using ODP.Web.UI.Models.ViewModels.Fazenda;

namespace ODP.Web.UI.Models.Interfaces.Fazenda
{
    public interface INFEletronicaService : IRepositoryService<NFEletronicaViewModel>,
                                            IBuscarDados<NFEletronicaViewModel>
    {

    }
}
