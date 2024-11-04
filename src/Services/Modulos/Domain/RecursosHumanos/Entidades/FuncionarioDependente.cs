namespace Domain.RecursosHumanos.Entidades
{
    public class FuncionarioDependente
    {
        public Guid FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        public Guid DependenteId { get; set; }
        public Dependente Dependente { get; set; }
    }
}
