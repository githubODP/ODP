using CGEODP.Core.DomainObjects;
using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Infra.Contratos.RepositoriesRead
{
    public class ContratosInternoRepositoryRead : RepositoryRead<ContratosInternos>, IContratosInternoRepositoryRead, IAggregateRoot
    {
        public ContratosInternoRepositoryRead(ObservatorioContext context) : base(context) { }
    }
}
