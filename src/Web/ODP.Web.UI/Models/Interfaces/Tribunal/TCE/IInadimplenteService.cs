﻿using ODP.Web.UI.Models.ViewModels.Dividas;
using ODP.Web.UI.Models.ViewModels.Tribunal.TCE;

namespace ODP.Web.UI.Models.Interfaces.Tribunal.TCE

{
    public interface IInadimplenteService : IRepositoryService<InadimplenteViewModel>,
                                            IBuscarDados<InadimplenteViewModel>
    {


    }
}