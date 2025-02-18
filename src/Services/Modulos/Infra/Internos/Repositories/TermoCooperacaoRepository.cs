using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Infra.Internos.Repositories
{
    public class TermoCooperacaoRepository : Repository<TermoCooperacao>, ITermoCooperacaoRepository
    {
        public TermoCooperacaoRepository(ObservatorioContext context) : base(context) { }
    }
}
