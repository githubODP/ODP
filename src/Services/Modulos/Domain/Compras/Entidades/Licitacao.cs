
using CGEODP.Core.DomainObjects;


namespace Domain.Compras.Entidades
{

    public class Licitacao : Entity, IAggregateRoot

    {
        public string? Protocolo { get; set; }
        public string Orgao { get; set; } 
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string? Fornecedor { get; set; }
        public string? CNPJ { get; set; }
        public string? CPF { get; set; }
        public string Modalidade { get; set; } 
        public string Situacao { get; set; } 
        public string? Objeto { get; set; }
        public float? ValorEstimado { get; set; }
        public float? ValorLicitado { get; set; }
        public float? ValorHomologado { get; set; }

    }
}
