
using Domain.Corregedoria.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODP.Web.UI.Models.Corregedoria
{
    public class InstauracaoViewModel
    {
        public Guid Id { get; set; }
        
        public int Ano { get; set; }

        [Display(Name = "Investigado")]
        
        public string Nome { get; set; }
       
        [Display(Name = "CPF/CNPJ")]

        public string CNPJCPF { get; set; }
        public string RG { get; set; }

        
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Orgão")]
        public ETipoOrgao Orgao { get; set; }

        
        [Display(Name = "Procedimento")]

        [Column(TypeName = "nvarchar(50)")]
        public ETipoProcedimento Procedimento { get; set; }

        
        [Display(Name = "Protocolo")]
        [Column("Protocolo")]
        public string Protocolo { get; set; }

       
        public string Objeto { get; set; }

        
        [Display(Name = "Ato Normativo")]
        [Column("AtoNormativo")]
        public string AtoNormativo { get; set; }

        
        [Display(Name = "Data Publicação")]
        [Column("DataPublicacao")]
        public DateTime? DataPublicacao { get; set; }

       
        [Display(Name = "Nº DIOE")]
        [Column("NumeroDIOE")]
        public int? NumeroDIOE { get; set; }

        [Display(Name = "Ato Normativo Decisão")]
        [Column("AtoNormativoDecisao")]
        public string AtoNormativoDecisao { get; set; }

        [Display(Name = "Data Publicação Decisão")]
        [Column("DataPublicacaoDecisao")]
        public DateTime? DataPublicacaoDecisao { get; set; }

        [Display(Name = "Nº DIOE Decisão")]
        [Column("NumeroDIOEDecisao")]
        public int? NumeroDIOEDecisao { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Decisão")]

        public ETipoDecisao Decisao { get; set; }

        [Display(Name = "Info Adicionais")]
        [Column("InfoAdd")]
        public string InfoAdd { get; set; }
    }
}
