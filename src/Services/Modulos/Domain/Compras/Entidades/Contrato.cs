using CGEODP.Core.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace Domain.Compras.Entidades
{
    public class Contrato : Entity, IAggregateRoot
    {
        
        public int AnoContrato { get; set; }       
        public string OrgaoGestor { get; set; }       
        public string OrgaoGMS { get; set; }       
        public string StatusContrato { get; set; }
        public string? Fornecedor { get; set; }      
        public string? CNPJ { get; set; }
        public string? CPF { get; set; }
        public string? Protocolo { get; set; }
       public string NumeroContrato { get; set; }       
        public string TipoContrato { get; set; }       
        public string Fiscal { get; set; }
        public string? TermoAditivo { get; set; }      
        public string? Empenho { get; set; } 
        public int QtdeAditivo { get; set; }
        public float VlrTotalOriginal { get; set; }
        public float VlrTotalAtual { get; set; }
        public string? ObjetoContrato { get; set; }
        public string? Justificativa { get; set; }
        public DateTime? DTInicioVigencia { get; set; }
        public DateTime? DTFimVigencia { get; set; }
    }
}
