using CGEODP.Core.DomainObjects;

namespace Domain.GovernoFederal.Entidades
{
    public class TrabalhoEscravo : Entity, IAggregateRoot
    {
        public int? Ano { get; set; }
        public string UF { get; set; }
        public string Empregador { get; set; }
        public string CNPJ { get; set; }

        public string CPF { get; set; }
        public string Estabelecimento { get; set; }
        public int? TrabalhadoresEnvolvidos { get; set; }
        public string? CNAE { get; set; }
        public DateTime DTDecisaoAdm { get; set; }
        public DateTime DTInclusao { get; set; }

    }
}
