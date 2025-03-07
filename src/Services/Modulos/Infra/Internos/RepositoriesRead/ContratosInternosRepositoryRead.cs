using CGEODP.Core.DomainObjects;
using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.Internos.RepositoriesRead
{
    public class ContratosInternosRepositoryRead : RepositoryRead<ContratosInternos>, IContratosInternosRepositoryRead, IAggregateRoot
    {
        public ContratosInternosRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<PagedResult<ContratosInternos>> Listar(
           int pageNumber,
           int pageSize,
           string termo = null)
        {

            var query = _context.Set<ContratosInternos>().AsQueryable();

            if (!string.IsNullOrEmpty(termo))
            {


                query = query.Where(i =>
                    i.Contrato.Contains(termo) ||
                    i.NroContrato.Contains(termo) ||
                    i.Protocolo.Contains(termo) ||
                    i.Objeto.Contains(termo) ||
                    i.Gestor.Contains(termo));

            }
            var items = await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new PagedResult<ContratosInternos>
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
