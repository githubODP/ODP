using CGEODP.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace Domain.Compras.Entidades
{
    public class Contrato : Entity, IAggregateRoot
    {
        [Display(Name = "Ano Contrato")]
        public int AnoContrato { get; set; }

        [Display(Name = "Orgão Gestor")]
        public string OrgaoGestor { get; set; }
        [Display(Name = "Orgão GMS")]
        public string OrgaoGMS { get; set; }
        [Display(Name = "Status Contrato")]
        public string StatusContrato { get; set; }
        public string Fornecedor { get; set; }
        [Display(Name = "CNPJ/CPF Fornecedor")]
        public string CNPJCPF { get; set; }
        public string Protocolo { get; set; }
        [Display(Name = "Número Contrato")]
        public string NumeroContrato { get; set; }
        [Display(Name = "Tipo Contrato")]
        public string TipoContrato { get; set; }

        [Display(Name = "Termo Aditivo")]

        public string Fiscal { get; set; }
        public string TermoAditivo { get; set; }
        [Display(Name = "Numero Empenho")]
        public string Empenho { get; set; }

        [Display(Name = "Ano Empenho")]
        public int? AnoEmpenho { get; set; }

        [Display(Name = "Numero Liquidacao")]
        public string? Liquidacao { get; set; }

        [Display(Name = "Data Liquidacao")]
        public DateTime? DTLiquidacao { get; set; }


        [Display(Name = "Data Pagamento")]
        public DateTime? DTPagamento { get; set; }


        [Display(Name = "Quantidade Aditivo")]
        public int QtdeAditivo { get; set; }

        [Display(Name = "Valor Total Original")]
        public float VlrTotalOriginal { get; set; }

        [Display(Name = "Valor Total Atual")]
        public float VlrTotalAtual { get; set; }

        [Display(Name = "Objeto Contrato")]
        public string ObjetoContrato { get; set; }

        public string Justificativa { get; set; }
        [Display(Name = "Data Início Vigência")]

        public DateTime? DTInicioVigencia { get; set; }
        [Display(Name = "Data Fim Vignência")]
        public DateTime? DTFimVigencia { get; set; }
    }
}
