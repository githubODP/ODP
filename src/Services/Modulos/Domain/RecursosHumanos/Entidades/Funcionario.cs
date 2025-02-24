#nullable disable
using CGEODP.Core.DomainObjects;

namespace Domain.RecursosHumanos.Entidades
{
    public class Funcionario : Entity, IAggregateRoot
    {

        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime DtNascimento { get; set; }
        public int Idade { get; set; }
        public string NomeMae { get; set; }
        public int Ordinal { get; set; }
        public string Orgao { get; set; }
        public int? Ano { get; set; }
        public int? Mes { get; set; }
        public string TipoRH { get; set; }
        public string Quadro { get; set; }
        public string? Lotacao { get; set; }
        public string? Cargo { get; set; }
        public string? Simbolo { get; set; }
        public DateTime? Inicio { get; set; }
        public string DescricaoInicio { get; set; }
        public DateTime? Desligamento { get; set; }
        public string MotivoDesligamento { get; set; }
        public int? TempoServico { get; set; }
        public int? CargaHoraria { get; set; }
        public string IdOrigem { get; set; }
        public string OrdinalOrigem { get; set; }
        public string IDUnico { get; set; }
        public string? FGP { get; set; }
        public string Funcao { get; set; }

        public ICollection<Dependente> Dependentes { get; set; } = new List<Dependente>();


        // Relacionamento um-para-muitos com Dependente
        //public ICollection<FuncionarioDependente> FuncionarioDependentes { get; set; }

        //Relacionamento um-para-muitos com Ocorrencia
        //public ICollection<FuncionarioOcorrencia> FuncionarioOcorrencias { get; set; }

        //Relacionamento muitos-para-muitos com Rubrica
        //public ICollection<FuncionarioRubrica> FuncionarioRubricas { get; set; }

    }

}
