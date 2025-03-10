using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ODP.Web.UI.Models.Internos
{
    public class DemandaViewModel
    {

        public Guid Id { get; set; }
        public int Ano { get; set; }

        [Display(Name = "Mês")]
        public int Mes { get; set; }

        [Display(Name = "Nome do Documento")]
        public string NomeDocto { get; set; }
        public string Protocolo { get; set; }

        [Display(Name = "Objeto de Análise")]
        [Column("Objeto")]
        public string Objeto { get; set; }

        [Display(Name = "Solicitação de Análise")]
        public string Solicitacao { get; set; }

        [Display(Name = "Orgão Investigado")]
        public string Orgao { get; set; }

        [Display(Name = "Data de Recebimento")]
        public DateTime DataRecebe { get; set; }

        [Display(Name = "Data de Resposta")]
        public DateTime DataResposta { get; set; } = DateTime.Now;

        [Display(Name = "Tempo de Resposta (Dias)")]
        public int Tempo => (DataResposta - DataRecebe).Days;

        [Display(Name = "Area Solicitante")]
        public string Solicitante { get; set; }

        [Display(Name = "Nome do Solicitante")]
        public string NomeSolicita { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}
