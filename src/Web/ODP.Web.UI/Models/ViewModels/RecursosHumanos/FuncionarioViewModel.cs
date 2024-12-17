using System;
using System.Collections.Generic;

namespace ODP.Web.UI.Models.ViewModels.RecursosHumanos
{
    public class FuncionarioViewModel
    {
        public Guid Id { get; set; }
        public string Ordinal { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Orgao { get; set; }
        public string Simbolo { get; set; }
        public string Quadro { get; set; }
        public string Lotacao { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Desligamento { get; set; }

        // Lista de dependentes
        public List<DependenteViewModel> Dependentes { get; set; } = new List<DependenteViewModel>();

    }
}
