
using Domain.Corregedoria.Entidade;
using Domain.Corregedoria.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Infra.Corregedoria.Repositories
{
    public class InstauracaoRepository : Repository<Instauracao>, IInstauracaoRepository
    {

        public InstauracaoRepository(ObservatorioContext context) : base(context) { }
    }
}
