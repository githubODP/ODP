using Domain.Tribunal.Entidades.TSE;


namespace Domain.ConsultaCNPJCPF.DTO.Tribunais.TSE
{
    public class BuscaTSEDTO
    {
        public List<DoacaoCandidato> DoacaoCandidato { get; set; }
        public List<DoacaoGeral> DoacaoGeral { get; set; }
        public List<DoacaoPartido> DoacaoPartido { get; set; }
        public List<DoacaoPartidoGeral> DoacaoPartidoGeral { get; set; }
        public List<FornecedorCandidato> FornecedorCandidato { get; set; }
        public List<FornecedorPartido> FornecedorPartido { get; set; }

    }
}
