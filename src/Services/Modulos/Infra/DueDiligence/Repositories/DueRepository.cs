using Domain.DueDiligence.Entidade;
using Domain.DueDiligence.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Infra.DueDiligence.Repositories
{
    public class DueRepository : Repository<Comissionado>, IDueRepository
    {

        public DueRepository(ObservatorioContext context) : base(context) { }
    }
}
