using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.RepositoryExterno
{
    public class RepositoryRead<T> : IRepositoryRead<T> where T : class, IAggregateRoot
    {
        protected readonly ObservatorioContext _context;

        public RepositoryRead(ObservatorioContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;




        public async Task<T> ObterId(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<PagedResult<T>> Listar(int pageNumber, int pageSize)
        {
            IQueryable<T> query = _context.Set<T>();


            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var results = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Results = results,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords
            };
        }

        public void Dispose()
        {
            _context?.Dispose();
        }


    }
}
