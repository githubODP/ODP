namespace Domain.RecursosHumanos.Entidades
{
    public class FuncionarioOcorrencia
    {
        public Guid FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        public Guid OcorrenciaId { get; set; }
        public Ocorrencia Ocorrencia { get; set; }
    }
}
