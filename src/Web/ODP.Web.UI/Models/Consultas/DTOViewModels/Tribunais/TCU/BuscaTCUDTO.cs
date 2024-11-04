using ODP.Web.UI.Models.ViewModels.Tribunal.TCU;
using System.Collections.Generic;


namespace ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TCU
{
    public class BuscaTCUDTO
    {
        public List<ContaEleitoralIrregularViewModel> ContaEleitoralIrregular { get; set; }
        public List<ContaIrregularViewModel> ContaIrregular { get; set; }
        public List<InabilitadoViewModel> Inabilitado { get; set; }
        public List<InidoneoViewModel> Inidoneo { get; set; }
    }
}
