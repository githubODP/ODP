using System;

namespace ODP.Web.UI.Models.ViewModels.Tribunal.TSE
{
    public class FornecedorPartidoViewModel
    {
        public Guid Id { get; set; }
        public int AnoEleicao { get; set; }
        public string UF { get; set; }
        public string Municipio { get; set; }
        public string CNPJPartido { get; set; }
        public string SiglaPartido { get; set; }
        public string Partido { get; set; }
        public string CNPJCPF { get; set; }
        public string Fornecedor { get; set; }
        public string DescricaoCargoFornecedor { get; set; }
        public string PartidoFornecedor { get; set; }
        public string DescricaoDespesas { get; set; }
        public float ValorDespesas { get; set; }
    }
}
