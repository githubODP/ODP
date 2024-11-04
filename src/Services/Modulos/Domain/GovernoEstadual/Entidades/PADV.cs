using CGEODP.Core.DomainObjects;


namespace Domain.GovernoEstadual.Entidades
{
    public class PADV : Entity, IAggregateRoot
    {

        public string Agencia { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public float ValorBruto { get; set; }
        public float ValorPago { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cidade { get; set; }
        public string NumeroPADV { get; set; }
        public string OrgaoSolicitante { get; set; }
        public string CNPJExecutor { get; set; }
        public string OrgaoPagador { get; set; }
        public string Objetivo { get; set; }
        public string SituacaoFornecedor { get; set; }
        public string SituacaoPADV { get; set; }


    }
}
