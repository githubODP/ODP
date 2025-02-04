using CGEODP.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace Domain.TermoCooperacao.Entidade
{
    public class TermoCooperacao : Entity, IAggregateRoot
    {
        public string Celebrante { get; set; }
        public string Celebrado { get; set; }
        public string Objeto { get; set; }
        public DateTime? DataCelebracao { get; set; }
        public DateTime? InicioVigencia { get; set; }
        public DateTime? FimVIgencia { get; set; }

    }
}