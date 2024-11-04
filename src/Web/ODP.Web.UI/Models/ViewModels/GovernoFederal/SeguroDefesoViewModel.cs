using System;

namespace ODP.Web.UI.Models.ViewModels.GovernoFederal
{
    public class SeguroDefesoViewModel
    {
        public Guid Id { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int CodigoMunicipio { get; set; }
        public string NomeMunicipio { get; set; }
        public string UF { get; set; }
        public string? CPF { get; set; }
        public int NISFavorecido { get; set; }
        public string CPFFavorecido { get; set; }
        public string NomeFavorecido { get; set; }
        public float ValorParcela { get; set; }
    }
}
