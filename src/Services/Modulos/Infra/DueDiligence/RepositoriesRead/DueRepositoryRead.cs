


using CGEODP.Core.DomainObjects;
using Domain.DueDiligence.Entidade;
using Domain.DueDiligence.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.DueDiligence.RepositoriesRead
{
    public class DueRepositoryRead : RepositoryRead<Comissionado>, IDueRepositoryRead, IAggregateRoot
    {
        public DueRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<List<Comissionado>> BuscarPorCPF(string cpf)
        {
            return await _context.Set<Comissionado>()
                .AsNoTracking()
                .Where(c => c.CPF == cpf)
                .ToListAsync();
        }
    }
}

