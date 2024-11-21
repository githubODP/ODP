using System;

namespace ODP.Web.UI.Models.ViewModels.GovernoFederal
{
    public class AcordoLenienciaViewModel
    {
        public Guid Id { get; set; }
        public string IdentificacaoAcordo { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataInicioAcordo { get; set; }
        public DateTime? DataFimAcordo { get; set; }
        public string Situacao { get; set; }
        public string NumeroProcesso { get; set; }
        public string TermosAcordo { get; set; }
        public string OrgaoSancionador { get; set; }
        public string Efeitos { get; set; }
        public string? Complementos { get; set; }
    }
}
