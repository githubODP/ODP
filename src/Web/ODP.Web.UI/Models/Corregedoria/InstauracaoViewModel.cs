
using Domain.Corregedoria.Enum;
using System;
using System.ComponentModel.DataAnnotations;


namespace ODP.Web.UI.Models.Corregedoria
{
    public class InstauracaoViewModel
    {
        public Guid Id { get; set; }

        public int Ano { get; set; }

        [Display(Name = "CNPJ/CPF")]
        public string? CNPJCPF { get; set; }
        public string? RG { get; set; }

        [Display(Name = "Órgão")]
        public ETipoOrgao Orgao { get; set; }
        public ETipoProcedimento Procedimento { get; set; }
        public string? Protocolo { get; set; }
        public string? Objeto { get; set; }

        [Display(Name = "Ato Normativo")]
        public string? AtoNormativo { get; set; }

        [Display(Name = "Data Publicação")]
        public DateTime? DataPublicacao { get; set; }

        [Display(Name = "Nº DIOE")]
        public int? NumeroDIOE { get; set; }

        [Display(Name = "Ato Normativo Decisão")]
        public string? AtoNormativoDecisao { get; set; }

        [Display(Name = "Data Publicação Decisão")]
        public DateTime? DataPublicacaoDecisao { get; set; }

        [Display(Name = "Nº DIOE Decisão")]
        public int? NumeroDIOEDecisao { get; set; }

        [Display(Name = "Decisão")]
        public ETipoDecisao Decisao { get; set; }
        public string? InfoAdd { get; set; }

        [Display(Name = "Obrigação")]
        public string? Obrigacao { get; set; }

        [Display(Name = "Data Inicio TAC")]
        public DateTime? DataInicioTac { get; set; }

        [Display(Name = "Data Fim TAC")]
        public DateTime? DataFimTac { get; set; }

        [Display(Name = "Prazo Encerramento")]
        public int? PrazoEncerra { get; set; }
        [Display(Name = "PGE")]
        public bool? PGE { get; set; } = false;

        [Display(Name = "Cumpriu")]
        public bool? Cumpriu { get; set; } = false;

        [Display(Name = "Observação Ajustes TAC")]
        public string? ObservacaoAjusteTAC { get; set; }
    }
}
