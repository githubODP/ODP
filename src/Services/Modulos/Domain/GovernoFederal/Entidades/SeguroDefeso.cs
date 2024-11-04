using CGEODP.Core.DomainObjects;

namespace Domain.GovernoFederal.Entidades
{
    public class SeguroDefeso : Entity, IAggregateRoot
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int CodigoMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public string UF { get; set; }
        public string? CPF { get; set; }
        public string NISFavorecido { get; set; }
        public string RGPFavorecido { get; set; }
        public string NomeFavorecido { get; set; }
        public float ValorParcela { get; set; }
    }
}
