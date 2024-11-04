using ODP.Web.UI.Models.ViewModels.Tribunal.TCE;
using System.Collections.Generic;

namespace ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TCE
{
    public class BuscaTCEDTO
    {
        public List<CNPJRestritoViewModel> CNPJRestrito { get; set; }
        public List<CPFRestritoViewModel> CPFRestrito { get; set; }
        public List<InadimplenteViewModel> Inadimplente { get; set; }
        public List<IrregularidadeViewModel> Irregularidade { get; set; }
    }
}
