

using ODP.Web.UI.Models.ViewModels.GovernoFederal;
using System.Collections.Generic;

namespace ODP.Web.UI.Models.Consultas.DTOViewModels.GovernoFederal
{
    public class BuscaFederalDTO
    {
        public List<AcordoLenienciaViewModel> AcordosLeniencia { get; set; }
        public List<AeronaveViewModel> Aeronave { get; set; }
        public List<BeneficioContinuoViewModel> BeneficioContinuo { get; set; }
        public List<CeisViewModel> Ceis { get; set; }
        public List<ExpulsoFederalViewModel> ExpulsoFederal { get; set; }
        public List<PEPViewModel> PEP { get; set; }
        public List<SeguroDefesoViewModel> SeguroDefeso { get; set; }
        public List<TrabalhoEscravoViewModel> TrabalhoEscravo { get; set; }
        public List<BolsaFamiliaViewModel> BolsaFamilia { get; set; }
        public List<CnepViewModel> Cnep { get; set; }
        public List<CepimViewModel> Cepim { get; set; }

    }
}
