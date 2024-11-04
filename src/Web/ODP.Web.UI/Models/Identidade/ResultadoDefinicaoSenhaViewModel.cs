using System.Collections.Generic;

namespace ODP.Web.UI.Models.Identidade
{
    public class ResultadoRedefinicaoSenhaViewModel
    {
        public bool Sucesso { get; set; }
        public IEnumerable<string> Erros { get; set; }
    }
}
