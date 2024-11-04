

using ODP.Web.UI.Models.ViewModels.Dividas;
using System.Collections.Generic;

namespace ODP.Web.UI.Models.Consultas.DTOViewModels.Dividas
{
    public class BuscaDividasDTO
    {
        public List<DividaFGTSViewModel> DividaFGTS { get; set; }
        public List<DividaNaoPrevidenciariaViewModel> DividaNaoPrevidenciaria { get; set; }
        public List<DividaPrevidenciariaViewModel> DividaPrevidenciaria { get; set; }
    }
}
