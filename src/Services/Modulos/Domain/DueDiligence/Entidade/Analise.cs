using CGEODP.Core.DomainObjects;
using Domain.DueDiligence.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DueDiligence.Entidade
{
    public class Analise : Entity, IAggregateRoot
    {
        public string? NroProtocolo { get; set; }
        public DateTime DataAnalise { get; set; }

        [Display(Name = "Análise Técnica")]
        public string AnaliseTecnica { get; set; }
        public ETipoRisco Risco { get; set; }
        public ETipoRessalva Ressalvas { get; set; }

        public string Responsavel { get; set; }

        [ForeignKey("Comissionado")]
        public Guid ComissionadoId { get; set; }
        public Comissionado Comissionado { get; set; }
    }
}
