using CGEODP.Core.DomainObjects;

namespace Domain.Tribunal.Entidades.TCU
{
    public class Inabilitado : Entity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Processo { get; set; }
        public string Deliberacao { get; set; }
        public DateTime? TransitoJulgado { get; set; }
        public DateTime DataFinal { get; set; }
        public DateTime DataAcordao { get; set; }
    }
}
