using Domain.Tribunal.Entidades.TCU;


namespace Domain.ConsultaCNPJCPF.DTO.Tribunais.TCU
{
    public class BuscaTCUDTO
    {
        public List<ContaEleitoralIrregular> ContaEleitoralIrregular { get; set; }
        public List<ContaIrregular> ContaIrregular { get; set; }
        public List<Inabilitado> Inabilitado { get; set; }
        public List<Inidoneo> Inidoneo { get; set; }
    }
}
