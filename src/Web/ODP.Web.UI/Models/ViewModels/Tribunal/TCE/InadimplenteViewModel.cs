using System;

namespace ODP.Web.UI.Models.ViewModels.Tribunal.TCE
{
    public class InadimplenteViewModel
    {
        public Guid Id { get; set; }
        public string Devedor { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string Credor { get; set; }
        public string Processo { get; set; }
        public string Decisao { get; set; }
        public string TipoSancao { get; set; }
        public string Referencia { get; set; }
        public float Valor { get; set; }
        public float ValorRecolhido { get; set; }
        public float SaldoDevedor { get; set; }
        public float Execução { get; set; }
    }
}
