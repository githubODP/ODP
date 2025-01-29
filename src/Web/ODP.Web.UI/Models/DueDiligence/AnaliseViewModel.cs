using Domain.DueDiligence.Enum;
using System;

namespace ODP.Web.UI.Models.DueDiligence
{
    public class AnaliseViewModel
    {
       
        public Guid Id { get; set; }
        public string NroProtocolo { get; set; }
        public string DataAnalise { get; set; } // Formatar como string para exibição
        public string AnaliseTecnica { get; set; }
        public ETipoRisco Risco { get; set; }
        public ETipoRessalva Ressalvas { get; set; }
        public string Responsavel { get; set; }

            // Dados adicionais do protocolo
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Orgao { get; set; }
        public string Observacao { get; set; }
       


    }
}
