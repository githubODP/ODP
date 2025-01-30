using Domain.DueDiligence.Enum;
using System;

namespace ODP.Web.UI.Models.DueDiligence
{
    public class AnaliseCadastroViewModel
    {

        public Guid Id { get; set; }
        public string NroProtocolo { get; set; }
        public DateTime DataAnalise { get; set; }
        public string AnaliseTecnica { get; set; }
        public ETipoRisco Risco { get; set; }
        public ETipoRessalva Ressalvas { get; set; }
        public string Responsavel { get; set; }
    }
}
