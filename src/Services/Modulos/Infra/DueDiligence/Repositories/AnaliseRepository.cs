using Domain.DueDiligence.Entidade;
using Domain.DueDiligence.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Infra.DueDiligence.Repositories
{
    public class AnaliseRepository : Repository<Analise>, IAnaliseRepository
    {

        public AnaliseRepository(ObservatorioContext context) : base(context) { }



    }
}
