using Domain.Library.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Library.Entidades
{
    [NotMapped]
    public class DocumentoInternoViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Data do Documento")]
        public DateTime DataDocumento { get; set; }

        public string Motivo { get; set; }

        [Display(Name = "Dado de Busca")]
        public string DadoBusca { get; set; }

        [Display(Name = "Documento em Anexo")]
        public IFormFile DocumentoAnexo { get; set; }

        [Display(Name = "Solicitação")]
        public string Solicitacao { get; set; }

        [Display(Name = "Tipo de Solicitação")]
        public ETipoSolicitacao Tipo { get; set; }

    }
}
