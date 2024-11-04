using System;

namespace ODP.Web.UI.Models.ViewModels.Dividas
{
    public class DividaFGTSViewModel
    {
        public Guid Id { get; set; }
        public string CNPJCPF { get; set; }
        public string TipoPessoa { get; set; }
        public string TipoDevedor { get; set; }
        public string NomeDevedor { get; set; }
        public string UFUnidadeResponsavel { get; set; }
        public string UnidadeResponsavel { get; set; }
        public string EntidadeResponsavel { get; set; }
        public string UnidadeInscricao { get; set; }
        public string NumeroInscricao { get; set; }
        public string TipoSituacaoInscricao { get; set; }
        public string SituacaoInscricao { get; set; }
        public string ReceitaPrincipal { get; set; }
        public DateTime DataInscricao { get; set; }
        public string IndicadorAjuizado { get; set; }
        public float ValorConsolidado { get; set; }
    }
}
