using System;

namespace ODP.Web.UI.Models.ViewModels.Compras
{
    public class LicitacaoViewModel
    {
        public Guid Id { get; set; }
        public string Protocolo { get; set; }
        public string Orgao { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string Fornecedor { get; set; }
        public string CNPJCPF { get; set; }
        public string Modalidade { get; set; }
        public string Situacao { get; set; }
        public string Objeto { get; set; }
        public float ValorEstimado { get; set; }
        public float? ValorLicitado { get; set; }
        public float ValorHomologado { get; set; }
    }
}
