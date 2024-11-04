using CGEODP.Core.DomainObjects;

namespace Domain.Tribunal.Entidades.TCE
{
    public class Irregularidade : Entity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Cargo { get; set; }
        public DateTime? InicioVigencia { get; set; }
        public DateTime? TerminoVigencia { get; set; }
        public string Decisao { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public string Processo { get; set; }
        public string Assunto { get; set; }
        public string Entidade { get; set; }
        public string? CNPJ { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
    }
}
