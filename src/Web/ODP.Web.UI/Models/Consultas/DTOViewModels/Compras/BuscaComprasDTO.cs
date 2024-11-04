
using ODP.Web.UI.Models.ViewModels.Compras;
using System.Collections.Generic;

namespace ODP.Web.UI.Models.Consultas.DTOViewModels.Compras
{
    public class BuscaComprasDTO
    {

        public List<ContratoViewModel> Contrato { get; set; }
        public List<DispensaViewModel> Dispensa { get; set; }
        public List<LicitacaoViewModel> Licitacao { get; set; }
    }
}
