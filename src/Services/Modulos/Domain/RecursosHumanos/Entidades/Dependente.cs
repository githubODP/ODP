

using CGEODP.Core.DomainObjects;

namespace Domain.RecursosHumanos.Entidades
{
    public class Dependente : Entity, IAggregateRoot
    {
        public string IDUnico { get; set; }
        public string Matricula { get; set; }
        public string Ordinal { get; set; }
        public string CPF { get; set; }
        public string Orgao { get; set; }
        public string MatriculaDependente { get; set; }
        public string CPFDependente { get; set; }
        public string RGDependente { get; set; }
        public string NomeDependente { get; set; }
        public DateTime DTNascDependente { get; set; }
        public string SexoDependente { get; set; }
        public string TipoDependente { get; set; }
        public DateTime FimDependente { get; set; }
        public int IdadeDependente { get; set; }
        public DateTime InicioDependente { get; set; }
        public Funcionario funcionario { get; set; }

        // Relacionamento com Funcionario
        //public Funcionario Funcionario { get; set; } 

        // Relacionamento muitos-para-muitos com Funcionario
        public ICollection<FuncionarioDependente> FuncionarioDependentes { get; set; }
    }
}
