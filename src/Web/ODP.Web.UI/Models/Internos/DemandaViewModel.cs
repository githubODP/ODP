using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODP.Web.UI.Models.Internos
{
    public class DemandaViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Ano")]
        [Column("Ano")]
        public int Ano { get; set; }

        [Display(Name = "Mes")]
        [Column("Mes")]
        public int Mes { get; set; }

        [Display(Name = "Nome do Documento")]
        [Column("NomeDocto")]
        public string NomeDocto { get; set; }

        [Display(Name = "Protocolo")]
        [Column("Protocolo")]
        public string Protocolo { get; set; }

        [Display(Name = "Objeto de Analise")]
        [Column("Objeto")]
        public string Objeto { get; set; }

        [Display(Name = "Solicitaçao de Analise")]
        [Column("Solicitacao")]
        public string Solicitacao { get; set; }

        [Display(Name = "Orgao Investigado")]
        [Column("Orgao")]
        public string Orgao { get; set; }

        [Display(Name = "Data de Recebimento")]
        [Column("DataRecebe")]
        public DateTime DataRecebe { get; set; }

        [Display(Name = "Data de Resposta")]
        [Column("DataResposta")]
        public DateTime DataResposta { get; set; } = DateTime.Now;

        [Display(Name = "Tempo de Resposta (Dias)")]
        [NotMapped] // Indica que essa propriedade não será mapeada no banco de dados
        public int Tempo => (DataResposta - DataRecebe).Days;

        [Display(Name = "Area Solicitante")]
        [Column("Solicitante")]
        public string Solicitante { get; set; }

        [Display(Name = "Nome do Solicitante")]
        [Column("NomeSolicita")]
        public string NomeSolicita { get; set; }


        [Display(Name = "Observacao")]
        [Column("Observacao")]
        public string Observacao { get; set; }

    }
}
