using CGEODP.Core.DomainObjects;

namespace Domain.GovernoFederal.Entidades
{
    public class BolsaFamilia : Entity, IAggregateRoot
    {
        public string CodigoMunicipioSIAFI { get; set; }
        public string NomeMunicipio { get; set; }
        public string UF { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Nis { get; set; }
        public int AnoReferencia { get; set; }
        public int MesReferencia { get; set; }
        public int MesCompetencia { get; set; }
        public int AnoCompetencia { get; set; }
        public float Valor { get; set; }
    }
}
