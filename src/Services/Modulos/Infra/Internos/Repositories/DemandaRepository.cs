using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Infra.Internos.Repositories
{
    public class DemandaRepository : Repository<DemandasInternas>, IDemandaRepository
    {

        public DemandaRepository(ObservatorioContext context) : base(context) { }
    }
}
