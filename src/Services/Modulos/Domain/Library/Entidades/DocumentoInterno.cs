using CGEODP.Core.DomainObjects;
using Domain.Library.Enum;
using System.ComponentModel.DataAnnotations;

namespace Domain.Library.Entidades
{
    public class DocumentoInterno : Entity, IAggregateRoot
    {
        [Display(Name = "Data do Documento")]
        public DateTime DataDocumento { get; set; }
        public string Motivo { get; set; }
        [Display(Name = "Dado de Busca")]
        public string DadoBusca { get; set; }
        [Display(Name = "Documento em Anexo")]
        public string DocumentoAnexo { get; set; }

        [Display(Name = "Solicitação")]
        public string Solicitacao { get; set; }

        [Display(Name = "Tipo de Solicitação")]
        public ETipoSolicitacao Tipo { get; set; }
    }
}
