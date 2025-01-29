using Domain.DueDiligence.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DueDiligence.Entidade
{
    public class AnaliseUpdateDTO
    {
        public Guid Id { get; set; } // Identificador da análise
        public DateTime DataAnalise { get; set; }
        public string AnaliseTecnica { get; set; }
        public ETipoRisco Risco { get; set; }
        public ETipoRessalva Ressalvas { get; set; }
        public string Responsavel { get; set; }
    }
}
