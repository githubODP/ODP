using System;

namespace ODP.Web.UI.Models.ViewModels.GovernoEstadual
{
    public class AmbientalViewModel
    {
        public Guid Id { get; set; }
        public string Municipio { get; set; }
        public string CNPJCPF { get; set; }
        public string Infrator { get; set; }
        public int QtdeInfracoes { get; set; }
        public string Situacao { get; set; }
        public DateTime? DTInscricaoSEFA { get; set; }
        public int AnoInfracao { get; set; }
        public float ValorAutuacao { get; set; }
        public float ValorOficioPago { get; set; }
        public int Infracao { get; set; }
    }
}
