using CGEODP.Core.DomainObjects;

namespace Domain.Tribunal.Entidades.TCU
{
    public class Inidoneo : Entity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string UF { get; set; }
        public string Processo { get; set; }
        public string Deliberacao { get; set; }
        public DateTime TransitoJulgado { get; set; }
        public DateTime DataFinal { get; set; }
        public DateTime DataAcordao { get; set; }
    }
}
