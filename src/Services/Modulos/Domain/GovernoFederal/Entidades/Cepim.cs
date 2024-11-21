using CGEODP.Core.DomainObjects;

namespace Domain.GovernoFederal.Entidades
{
    public class Cepim : Entity, IAggregateRoot
    {
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string NroConvenio { get; set; }
        public string Orgao { get; set; }
        public string Impedimento { get; set; }
    }
}
