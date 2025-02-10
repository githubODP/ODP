using Domain.Internos.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODP.Web.UI.Models.Cooperacao
{
    public class TermoCooperacaoViewModel
    {

        public Guid Id { get; set; }
        public string Protocolo { get; set; }
        [Display(Name = "Orgão")]
        public string Orgao { get; set; }
        public string Sigla { get; set; }
        [Display(Name = "Nº Termo")]
        public string NroTermo { get; set; }
        [Display(Name = "Ínicio Vigência")]
        public DateTime InicioVigencia { get; set; }
        [Display(Name = "Fim Vigência")]
        public DateTime FimVIgencia { get; set; }
        public int Validade { get; set; }
        public bool Ativo { get; set; }
        public ETipoStatus Status { get; set; }
        public bool Renovar { get; set; }
        public int DIOE { get; set; }
        [Display(Name = "Publicação")]
        public DateTime DataPublicacao { get; set; }
        public string Objeto { get; set; }
        [Display(Name = "Regulamentação")]
        public string Regulamentacao { get; set; }
        [Display(Name = "Informações")]
        public string Informacoes { get; set; }
        [Display(Name = "Observações")]
        public string Observacao { get; set; }
    }
}
