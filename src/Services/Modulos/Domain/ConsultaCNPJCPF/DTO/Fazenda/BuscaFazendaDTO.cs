using Domain.Fazenda.Entidades;


namespace Domain.ConsultaCNPJCPF.DTO.Fazenda
{
    public class BuscaFazendaDTO
    {
        public List<Jucepar> Jucepar { get; set; }
        public List<NFEletronica> NFEletronica { get; set; }
        public List<NFEletronicaFederal> NFEletronicaFederal { get; set; }
    }
}
