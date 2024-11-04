

using CGEODP.Core.DomainObjects;

namespace Domain.Tribunal.Entidades.TSE
{
    public class DoacaoPartidoGeral : Entity, IAggregateRoot
    {
        public int AnoEleicao { get; set; }
        public string IDPrestadorContas { get; set; }
        public string UF { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string SequenciaReceita { get; set; }
        public DateTime DataDoacao { get; set; }
        public string DescricaoReceita { get; set; }
        public float ValorReceita { get; set; }
        public string CNPJPartido { get; set; }
        public string NomePartido { get; set; }
        public string SiglaPartido { get; set; }

    }
}
