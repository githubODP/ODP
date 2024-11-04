using System;

namespace ODP.Web.UI.Models.ViewModels.Tribunal.TCU
{
    public class InidoneoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CNPJCPF { get; set; }
        public string UF { get; set; }
        public string Processo { get; set; }
        public string Deliberacao { get; set; }
        public DateTime TransitoJulgado { get; set; }
        public DateTime DataFinal { get; set; }
        public DateTime DataAcordao { get; set; }
    }
}
