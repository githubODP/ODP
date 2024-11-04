using CGEODP.Core.DomainObjects;

namespace Domain.GovernoFederal.Entidades
{
    public class ExpulsoFederal : Entity, IAggregateRoot
    {
        public string Esfera { get; set; }
        public string? Nome { get; set; }
        public string CPF { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public string Punicao { get; set; }
        public string? NumeroPortaria { get; set; }
        public string SecaoExtraDOU { get; set; }
        public string? NumeroProcesso { get; set; }
        public string Unidade { get; set; }
        public string? UF { get; set; }
        public string CargoEfetivo { get; set; }
        public string CargoComissao { get; set; }
        public string FundamentosSancao { get; set; }

    }
}
