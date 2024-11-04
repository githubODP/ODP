using CGEODP.Core.DomainObjects;

namespace Domain.GovernoFederal.Entidades
{
    public class Ceis : Entity, IAggregateRoot
    {
        public string Cadastro { get; set; }
        public int Codigo { get; set; }
        public string? TipoPessoa { get; set; }
        public string CNPJCPF { get; set; }
        public string NomeInformadoOrgaoSancionador { get; set; }
        public string? RazaoSocialReceita { get; set; }
        public string? NomeFantasiaReceita { get; set; }
        public string NroProcesso { get; set; }
        public string TipoSancao { get; set; }
        public DateTime? DTInicioSancao { get; set; }
        public DateTime? DTFimSancao { get; set; }
        public string OrgaoSancionador { get; set; }
        public string? UFOrgaoSancionador { get; set; }
        public DateTime? DTPublicacao { get; set; }
        public string Publicacao { get; set; }
        public string? Detalhamento { get; set; }
        public string AbrangenciaDecisao { get; set; }
        public string FundamentacaoLegal { get; set; }
        public DateTime? DTTransitoJulgado { get; set; }
    }
}
