
using CGEODP.Core.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.RecursosHumanos.Entidades
{
    public class Rubrica : Entity, IAggregateRoot
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string Matricula { get; set; }
        public int Ordinal { get; set; }
        public string CPF { get; set; }
        public string Orgao { get; set; }
        public string Codigo { get; set; }
        public string DescricaoRubrica { get; set; }
        public string TipoRubrica { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public Decimal Valor { get; set; }
        public string IDUnico { get; set; }



    }
}
