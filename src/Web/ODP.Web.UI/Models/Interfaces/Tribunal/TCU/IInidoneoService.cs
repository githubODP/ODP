﻿using ODP.Web.UI.Models.ViewModels.Dividas;
using ODP.Web.UI.Models.ViewModels.Tribunal.TCU;

namespace ODP.Web.UI.Models.Interfaces.Tribunal.TCU
{
    public interface IInidoneoService : IRepositoryService<InidoneoViewModel>,
                                        IBuscarDados<InidoneoViewModel>
    {


    }
}