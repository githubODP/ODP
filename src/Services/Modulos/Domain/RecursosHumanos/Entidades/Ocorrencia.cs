
using CGEODP.Core.DomainObjects;

namespace Domain.RecursosHumanos.Entidades
{
    public class Ocorrencia : Entity, IAggregateRoot
    {
        public string CPF { get; set; }
        public string Codigo { get; set; }
        public string DescricaoOcorrencia { get; set; }
        public DateTime InicioLicenca { get; set; }
        public int MesInicio { get; set; }
        public int AnoInicio { get; set; }
        public DateTime FimLicenca { get; set; }
        public int MesFim { get; set; }
        public int AnoFim { get; set; }
        public int Tempo { get; set; }
        public int TotalTempoLicenca { get; set; }
        public string Orgao { get; set; }
        public int Ordinal { get; set; }
        public string Matricula { get; set; }
        public string IDUnico { get; set; }

        //// Relacionamento com Funcionario
        //public Funcionario Funcionario { get; set; }

        // Relacionamento muitos-para-muitos com Funcionario
        public ICollection<FuncionarioOcorrencia> FuncionarioOcorrencias { get; set; }
    }
}