

using CGEODP.Core.DomainObjects;

namespace Domain.GovernoEstadual.Entidades
{
    public class Ambiental : Entity, IAggregateRoot
    {
        public string Municipio { get; set; }
        public string? CNPJ { get; set; }
        public string? CPF { get; set; }
        public string Infrator { get; set; }
        public int QtdeInfracoes { get; set; }
        public string Situacao { get; set; }
        public DateTime? DTInscricaoSEFA { get; set; }
        public int AnoInfracao { get; set; }
        public float ValorAutuacao { get; set; }
        public float ValorOficioPago { get; set; }
        public int Infracao { get; set; }

    }
}
