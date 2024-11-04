
using ODP.Web.UI.Models.ViewModels.Fazenda;
using System.Collections.Generic;

namespace ODP.Web.UI.Models.Consultas.DTOViewModels.Fazenda
{
    public class BuscaFazendaDTO
    {
        public List<JuceparViewModel> Jucepar { get; set; }
        public List<NFEletronicaFederalViewModel> NFEletronicaFederal { get; set; }
        public List<NFEletronicaViewModel> NFEletronica { get; set; }

    }
}
