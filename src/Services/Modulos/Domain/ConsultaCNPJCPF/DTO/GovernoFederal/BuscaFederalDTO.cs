using Domain.GovernoFederal.Entidades;


namespace Domain.ConsultaCNPJCPF.DTO.GovernoFederal
{
    public class BuscaFederalDTO
    {

        public List<Aeronave> Aeronave { get; set; }
        public List<BeneficioContinuo> BeneficioContinuo { get; set; }
        public List<Ceis> Ceis { get; set; }
        public List<ExpulsoFederal> ExpulsoFederal { get; set; }
        public List<SeguroDefeso> SeguroDefeso { get; set; }
        public List<TrabalhoEscravo> TrabalhoEscravo { get; set; }
        public List<AcordoLeniencia> AcordosLeniencia { get; set; }
        public List<PEP> PEP { get; set; }
        public List<BolsaFamilia> BolsaFamilia { get; set; }
        public List<Cnep> Cnep { get; set; }
        public List<Cepim> Cepim { get; set; }
    }
}
