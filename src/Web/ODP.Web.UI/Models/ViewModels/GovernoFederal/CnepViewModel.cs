using System;

namespace ODP.Web.UI.Models.ViewModels.GovernoFederal
{
    public class CnepViewModel
    {
        public Guid Id { get; set; }
        public string CodigoSancao { get; set; }
        public string CNPJCPF { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string NroProcesso { get; set; }
        public string CategoriaSancao { get; set; }
        public decimal ValorMulta { get; set; }
        public DateTime DataInicioSancao { get; set; }
        public DateTime? DataFimSancao { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string Publicacao { get; set; }
        public string Detalhamento { get; set; }
        public DateTime? DataTransitoJulgado { get; set; }
        public string Abrangencia { get; set; }
        public string OrgaoSancionador { get; set; }
        public string EsferaOrgao { get; set; }
        public string FundamentacaoLegal { get; set; }
    }
}
