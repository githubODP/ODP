using CGEODP.Core.DomainObjects;

namespace Domain.Graficos.Entidades
{
    public class Grafico : Entity, IAggregateRoot
    {
        public String Content { get; set; }
    }
}
