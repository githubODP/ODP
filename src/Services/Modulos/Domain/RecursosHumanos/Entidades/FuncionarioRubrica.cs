namespace Domain.RecursosHumanos.Entidades
{
    public class FuncionarioRubrica
    {
        public Guid FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        public Guid RubricaId { get; set; }
        public Rubrica Rubrica { get; set; }
    }
}
