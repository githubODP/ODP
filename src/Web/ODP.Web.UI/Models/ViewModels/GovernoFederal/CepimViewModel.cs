using System;

namespace ODP.Web.UI.Models.ViewModels.GovernoFederal
{
    public class CepimViewModel
    {
        public Guid Id { get; set; }
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public string NroConvenio { get; set; }
        public string Orgao { get; set; }
        public string Impedimento { get; set; }
    }
}
