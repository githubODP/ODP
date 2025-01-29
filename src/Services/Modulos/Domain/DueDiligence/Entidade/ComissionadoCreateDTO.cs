using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DueDiligence.Entidade
{
    public class ComissionadoCreateDTO
    {
        public string NroProtocolo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataResposta { get; set; }
        public string Observacao { get; set; }
    }
}
