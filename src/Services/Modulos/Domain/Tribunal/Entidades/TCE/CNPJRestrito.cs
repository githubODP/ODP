

using CGEODP.Core.DomainObjects;

namespace Domain.Tribunal.Entidades.TCE
{
    public class CNPJRestrito : Entity, IAggregateRoot
    {
        public string Municipio { get; set; }
        public string CNPJ { get; set; }
        public string NomeRazaoSocial { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string TipoSancao { get; set; }
        public string Situacao { get; set; }


    }
}
