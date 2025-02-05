using CGEODP.Core.DomainObjects;
using Domain.Corregedoria.Entidade;
using Domain.DueDiligence.Entidade;
using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.Internos.RepositoriesRead
{
    public class TermoCooperacaoRepositoryRead : RepositoryRead<TermoCooperacao>, ITermoCooperacaoRepositoryRead, IAggregateRoot
    {
        public TermoCooperacaoRepositoryRead(ObservatorioContext context) : base(context) { }




        public async Task<PagedResult<TermoCooperacao>> ListarComFiltrosAsync(int pageNumber, int pageSize)
        {
            var query = _context.Set<TermoCooperacao>().AsQueryable();

            var totalRecords = await query.CountAsync();

            var items = await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new PagedResult<TermoCooperacao>
            {
                Results = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        
        }
    }
}
