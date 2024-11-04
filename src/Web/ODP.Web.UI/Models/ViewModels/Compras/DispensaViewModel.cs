using System;

namespace ODP.Web.UI.Models.ViewModels.Compras
{
    public class DispensaViewModel
    {

        public Guid Id { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string Solicitacao { get; set; }
        public DateTime? DataDispensa { get; set; }
        public string Fornecedor { get; set; }
        public string CNPJCPF { get; set; }
        public string Item { get; set; }
        public float ValorDispensa { get; set; }
        public float ValorItem { get; set; }
        public int QtdItem { get; set; }
        public string Orgao { get; set; }
        public int CodigoItem { get; set; }
        public string Modalidade { get; set; }
        public string Objeto { get; set; }
        public string Protocolo { get; set; }
        public float QuantidadeXValor { get; set; }
    }
}
