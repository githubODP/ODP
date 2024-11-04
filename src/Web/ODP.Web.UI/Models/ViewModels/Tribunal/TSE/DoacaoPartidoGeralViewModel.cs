using System;

namespace ODP.Web.UI.Models.ViewModels.Tribunal.TSE
{
    public class DoacaoPartidoGeralViewModel
    {
        public Guid Id { get; set; }
        public int AnoEleicao { get; set; }
        public int IDPrestacaoContas { get; set; }
        public string UF { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public int SequenciaReceita { get; set; }
        public string DescricaoReceita { get; set; }
        public float ValorDoacao { get; set; }
        public string CNPJPartido { get; set; }
        public string NomePartido { get; set; }
        public string SiglaPartido { get; set; }
    }
}
