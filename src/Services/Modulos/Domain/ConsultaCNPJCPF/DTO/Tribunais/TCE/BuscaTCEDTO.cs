

using Domain.Tribunal.Entidades.TCE;

namespace Domain.ConsultaCNPJCPF.DTO.Tribunais.TCE
{
    public class BuscaTCEDTO
    {
        public List<CNPJRestrito> CNPJRestrito { get; set; }
        public List<CPFRestrito> CPFRestrito { get; set; }
        public List<Inadimplente> Inadimplente { get; set; }
        public List<Irregularidade> Irregularidade { get; set; }
    }
}
