using Domain.Contratos.Entidades;
using Domain.Contratos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Contratos.Repositories
{
    public class ContratosInternoRepository : Repository < ContratosInternos>, IContratosInternosRepository
    {
        public ContratosInternoRepository(ObservatorioContext context): base(context) { }
    }
}
