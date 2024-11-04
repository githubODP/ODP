using System;

namespace ODP.Web.UI.Models.ViewModels.GovernoFederal
{
    public class TrabalhoEscravoViewModel
    {
        public Guid Id { get; set; }
        public int Ano { get; set; }
        public string UF { get; set; }
        public string Empregador { get; set; }
        public string CNPJCPF { get; set; }
        public string Estabelecimento { get; set; }
        public int? TrabalhadoresEnvolvidos { get; set; }
        public string? CNAE { get; set; }
        public DateTime DTDecisaoAdm { get; set; }
        public DateTime DTInclusao { get; set; }
    }
}
