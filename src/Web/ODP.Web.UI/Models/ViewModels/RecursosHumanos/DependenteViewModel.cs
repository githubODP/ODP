using System;

namespace ODP.Web.UI.Models.ViewModels.RecursosHumanos
{
    public class DependenteViewModel
    {
        public Guid Id { get; set; }
        public string NomeDependente { get; set; }
        public string CPFDependente { get; set; }
        public string TipoDependente { get; set; }
        public DateTime? InicioDependente { get; set; }
        public DateTime? FimDependente { get; set; }

        // Dados do funcionário relacionado
        public FuncionarioViewModel Funcionario { get; set; }

    }
}
