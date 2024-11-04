using System;

namespace ODP.Web.UI.Models.ViewModels.GovernoFederal
{
    public class PEPViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Funcao { get; set; }
        public string Orgao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime? DataFimCarencia { get; set; }
    }
}
