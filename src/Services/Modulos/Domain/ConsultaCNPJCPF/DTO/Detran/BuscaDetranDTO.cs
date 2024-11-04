using Domain.Detran.Entidades;

namespace Domain.ConsultaCNPJCPF.DTO.Detran
{
    public class BuscaDetranDTO
    {
        public VeiculoBaixado VeiculoBaixado { get; set; }
        public VeiculoBloqueioRoubo VeiculoBloqueioRoubo { get; set; }
        public VeiculoCirculacao VeiculoCirculacao { get; set; }
        public VeiculoIndispAdmin VeiculoIndispAdmin { get; set; }
        public VeiculoOrdemJudicial VeiculoOrdemJudicial { get; set; }
        public VeiculoProntuario VeiculoProntuario { get; set; }
        public VeiculoRegistroFora VeiculoRegistroFora { get; set; }
    }
}
