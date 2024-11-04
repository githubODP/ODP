using System;

namespace ODP.Web.UI.Models.ViewModels.Tribunal.TCE
{
    public class CPFRestritoViewModel
    {
        public Guid Id { get; set; }
        public string Municipio { get; set; }
        public string CNPJCPF { get; set; }
        public string NomeRazaoSocial { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string TipoSancao { get; set; }
        public string Situacao { get; set; }
    }
}
