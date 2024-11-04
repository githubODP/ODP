using Domain.ConsultaCNPJCPF.DTO.Compras;
using Domain.ConsultaCNPJCPF.DTO.Detran;
using Domain.ConsultaCNPJCPF.DTO.Dividas;
using Domain.ConsultaCNPJCPF.DTO.DueDiligence;
using Domain.ConsultaCNPJCPF.DTO.Fazenda;
using Domain.ConsultaCNPJCPF.DTO.GovernoEstadual;
using Domain.ConsultaCNPJCPF.DTO.GovernoFederal;
using Domain.ConsultaCNPJCPF.DTO.RecursosHumanos;
using Domain.ConsultaCNPJCPF.DTO.Tribunais.TCE;
using Domain.ConsultaCNPJCPF.DTO.Tribunais.TCU;
using Domain.ConsultaCNPJCPF.DTO.Tribunais.TSE;

namespace Domain.ConsultaCNPJCPF.ResultadoConsulta
{
    public class ResultadoDTO
    {
        public BuscaComprasDTO BuscaCompras { get; set; }
        public BuscaDividasDTO BuscaDividas { get; set; }
        public BuscaFazendaDTO BuscaFazenda { get; set; }
        public BuscaDetranDTO BuscaDetran { get; set; }
        public BuscaDueDTO BuscaDue { get; set; }
        public BuscaEstadualDTO BuscaEstadual { get; set; }
        public BuscaFederalDTO BuscaFederal { get; set; }
        public BuscaRHDTO BuscaRH { get; set; }
        public BuscaTCUDTO BuscaTCU { get; set; }
        public BuscaTCEDTO BuscaTCE { get; set; }
        public BuscaTSEDTO BuscaTSE { get; set; }
        

    }
}
