﻿
using ODP.Web.UI.Models.ViewModels.Dividas;
using ODP.Web.UI.Models.ViewModels.GovernoFederal;

namespace ODP.Web.UI.Models.Interfaces.GovernoFederal
{
    public interface ICeisService : IRepositoryService<CeisViewModel>,
                                    IBuscarDados<CeisViewModel>
    {

    }
}