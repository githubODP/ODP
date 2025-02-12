using CGEODP.Core.DomainObjects;
using Domain.Contratos.Entidades;
using Domain.Contratos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Contratos.RepositoriesRead
{
    public class ContratosInternoRepositoryRead : RepositoryRead<ContratosInternos>, IContratosInternoRepositoryRead, IAggregateRoot
    {
        public ContratosInternoRepositoryRead(ObservatorioContext context): base(context) { }
    }
}
