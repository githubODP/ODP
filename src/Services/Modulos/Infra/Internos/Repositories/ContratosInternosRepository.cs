using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Infra.Internos.Repositories
{
    public class ContratosInternosRepository : Repository<ContratosInternos>, IContratosInternosRepository
    {
        public ContratosInternosRepository(ObservatorioContext context) : base(context) { }
    }
}
