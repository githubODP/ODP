using CGEODP.Core.DomainObjects;


namespace Domain.Tribunal.Entidades.TSE
{
    public class FornecedorCandidato : Entity, IAggregateRoot
    {
        public int AnoEleicao { get; set; }
        public string UF { get; set; }
        public string Municipio { get; set; }
        public string Candidato { get; set; }
        public string CPFCandidato { get; set; }
        public string DescricaoCargo { get; set; }
        public string Partido { get; set; }
        public string CNPJCPF { get; set; }
        public string Fornecedor { get; set; }
        public string DescricaoTipoDocto { get; set; }
        public string DescricaoDespesas { get; set; }
        public float ValorDespesas { get; set; }

    }
}
