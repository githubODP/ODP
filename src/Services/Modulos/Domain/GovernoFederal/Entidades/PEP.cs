using CGEODP.Core.DomainObjects;

namespace Domain.GovernoFederal.Entidades
{
    public class PEP : Entity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Funcao { get; set; }
        public string Orgao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime? DataFimCarencia { get; set; }
    }
}
