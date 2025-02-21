using Domain.Internos.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace ODP.Web.UI.Models.Internos
{
    public class ContratosInternosViewModel
    {
        public Guid Id { get; set; }
        public string Contrato { get; set; }
        [Display(Name = "Nro Contrato")]
        public string NroContrato { get; set; }

        [Display(Name = "Inicio Contrato ")]
        public DateTime InicioContrato { get; set; }

        [Display(Name = "Fim Contrato ")]
        public DateTime FimContrato { get; set; }
        public string Protocolo { get; set; }
        public float Valor { get; set; }
        public string Objeto { get; set; }
        public string Gestor { get; set; }
        public string Fiscal { get; set; }
        public int Dioe { get; set; }
        [Display(Name = "Data Publicação ")]
        public DateTime DataPublicacao { get; set; }
        public ETipoStatus Status { get; set; }
    }
}
