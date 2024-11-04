using ODP.Web.UI.Models.ViewModels.Tribunal.TSE;
using System.Collections.Generic;

namespace ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TSE
{
    public class BuscaTSEDTO
    {
        public List<DoacaoCandidatoViewModel> DoacaoCandidato { get; set; }
        public List<DoacaoGeralViewModel> DoacaoGeral { get; set; }
        public List<DoacaoPartidoGeralViewModel> DoacaoPartidoGeral { get; set; }
        public List<DoacaoPartidoViewModel> DoacaoPartido { get; set; }
        public List<FornecedorCandidatoViewModel> FornecedorCandidato { get; set; }
        public List<FornecedorPartidoViewModel> FornecedorPartido { get; set; }

    }
}
