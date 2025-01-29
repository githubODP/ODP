using Domain.DueDiligence.Entidade;
using Domain.DueDiligence.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.DueDiligence.Repositories
{
    public class AnaliseRepository : Repository<Analise>, IAnaliseRepository
    {

        public AnaliseRepository(ObservatorioContext context) : base(context) { }


        
    }
}
