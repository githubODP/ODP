using CGEODP.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace Domain.Fazenda.Entidades
{
    public class NFEletronicaFederal : Entity, IAggregateRoot
    {
        public string Serie { get; set; }
        public string NumeroNF { get; set; }

        [Display(Name = "Data Emissao")]
        [DataType(DataType.Date)]
        public DateTime DataEmissao { get; set; }
        public string? CNPJ { get; set; }
        public string? CPF { get; set; }
        public string Emitente { get; set; }
        public string UFEmitente { get; set; }
        public string MunicipioEmitente { get; set; }
        public string Destinatario { get; set; }
        public string CNPJDestinatario { get; set; }
        public string UFDestinatario { get; set; }
        public float ValorNF { get; set; }
        public string ChaveAcesso { get; set; }
        public string DescricaoProdutoServico { get; set; }
        public string Codigo { get; set; }
        public string? TipoProduto { get; set; }
        public int Quantidade { get; set; }
        public string? Unidade { get; set; }
        public float ValorUnitario { get; set; }
        public float ValorTotal { get; set; }
    }
}
