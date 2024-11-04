using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Infra.Data;


namespace Infra.RepositoryExterno
{
    public class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        protected readonly ObservatorioContext _context;

        public Repository(ObservatorioContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Atualizar(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Adicionar(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
