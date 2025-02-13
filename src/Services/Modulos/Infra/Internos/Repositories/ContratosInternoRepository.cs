using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Infra.Internos.Repositories
{
    public class ContratosInternoRepository : Repository<ContratosInternos>, IContratosInternosRepository
    {
        public ContratosInternoRepository(ObservatorioContext context) : base(context) { }
    }
}
