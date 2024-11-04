using CGEODP.Core.DomainObjects;

namespace Domain.GovernoFederal.Entidades
{
    public class BeneficioContinuo : Entity, IAggregateRoot
    {
        public int Ano { get; set; }
        public int MesCompetencia { get; set; }
        public int MesReferencia { get; set; }
        public int CodigoMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public string UFMunicipio { get; set; }
        public string? NisBeneficiario { get; set; }
        public string? CPF { get; set; }
        public string NomeBeneficiario { get; set; }
        public string? NISRepresentanteLegal { get; set; }
        public string? CPFRepresentanteLegal { get; set; }
        public string? NomeRepresentanteLegal { get; set; }
        public string? NumeroBeneficio { get; set; }
        public string BeneficioConcessaoJudicial { get; set; }
        public float ValorParcela { get; set; }

    }
}
