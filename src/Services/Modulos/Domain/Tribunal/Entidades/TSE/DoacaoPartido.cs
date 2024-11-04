using CGEODP.Core.DomainObjects;



namespace Domain.Tribunal.Entidades.TSE
{
    public class DoacaoPartido : Entity, IAggregateRoot
    {
        public int AnoEleicao { get; set; }
        public string IDPrestadorContas { get; set; }
        public string UF { get; set; }
        public string Municipio { get; set; }
        public string CNPJPrestadorConta { get; set; }
        public string SiglaPartido { get; set; }
        public string NomePartido { get; set; }
        public string DescricaoFonteReceita { get; set; }
        public string DescricaoOrigemReceita { get; set; }
        public string DescricaoEspecieReceita { get; set; }
        public string NomeDoador { get; set; }
        public string CNPJCPFDoador { get; set; }
        public string MunicipioDoador { get; set; }
        public string DescricaoCandidatoDoador { get; set; }
        public string SiglaPartidoDoador { get; set; }
        public string NomePartidoDoador { get; set; }
        public DateTime DataReceita { get; set; }
        public string DescricaoReceita { get; set; }
        public float ValorReceita { get; set; }
    }
}
