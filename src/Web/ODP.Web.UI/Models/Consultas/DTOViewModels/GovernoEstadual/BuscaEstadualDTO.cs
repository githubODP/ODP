
using ODP.Web.UI.Models.ViewModels.GovernoEstadual;
using System.Collections.Generic;

namespace ODP.Web.UI.Models.Consultas.DTOViewModels.GovernoEstadual
{
    public class BuscaEstadualDTO
    {

        public List<PADvViewModel> PADV { get; set; }
        public List<AmbientalViewModel> Ambiental { get; set; }
    }
}
