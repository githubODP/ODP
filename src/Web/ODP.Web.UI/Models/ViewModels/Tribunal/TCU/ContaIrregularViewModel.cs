using System;
using System.ComponentModel.DataAnnotations;

namespace ODP.Web.UI.Models.ViewModels.Tribunal.TCU
{
    public class ContaIrregularViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CNPJCPF { get; set; }
        public string Processo { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public string Deliberacao { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataJulgado { get; set; }
    }
}
