
using Domain.Corregedoria.Entidade;
using Domain.Corregedoria.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Infra.Corregedoria.Repositories
{
    public class InstauracaoRepository : Repository<Instauracao>, IInstauracaoRepository
    {

        public InstauracaoRepository(ObservatorioContext context) : base(context) { }

        //public async Task AddRangeAsync(IEnumerable<Instauracao> instauracoes)
        //{
        //    await _context.Set<Instauracao>().AddRangeAsync(instauracoes);
        //}

        //// Método para salvar as alterações no banco
        //public async Task SaveChangesAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}
    }
}
