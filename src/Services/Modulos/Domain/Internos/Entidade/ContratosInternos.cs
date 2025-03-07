using CGEODP.Core.DomainObjects;
using Domain.Internos.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Internos.Entidade
{
    public class ContratosInternos : Entity, IAggregateRoot
    {
        public string Contrato { get; set; }
        public string NroContrato { get; set; }
        public DateTime InicioContrato { get; set; }
        public DateTime FimContrato { get; set; }
        public string Protocolo { get; set; }
        public float Valor { get; set; }
        public string Objeto { get; set; }
        public string Gestor { get; set; }
        public string Fiscal { get; set; }
        public int Dioe { get; set; }
        public DateTime DataPublicacao { get; set; }
        public ETipoStatus Status { get; set; }

        [NotMapped]
        public int DiasRestantes => Math.Max((FimContrato - DateTime.Today).Days, 0);



    }
}
