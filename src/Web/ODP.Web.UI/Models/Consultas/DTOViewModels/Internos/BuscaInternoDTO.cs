﻿using ODP.Web.UI.Models.Demandas;
using ODP.Web.UI.Models.DueDiligence;
using ODP.Web.UI.Models.ViewModels.RecursosHumanos;
using System.Collections.Generic;

namespace ODP.Web.UI.Models.Consultas.DTOViewModels.Internos
{
    public class BuscaInternoDTO
    {
        public List<DueDiligenceViewModel> DueDiligence { get; set; }
        public List<DemandaViewModel> Demanda { get; set; }
        public List<FuncionarioViewModel> Funcionario { get; set; }
        public List<DependenteViewModel> Dependente { get; set; }

    }
}
