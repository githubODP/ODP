using CGEODP.Core.DomainObjects;
using Domain.RecursosHumanos.Entidades;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;


namespace Infra.RecursosHumanos.RepositoriesRead
{
    public class DependenteRepositoryRead : RepositoryRead<Dependente>, IDependenteRepositoryRead, IAggregateRoot
    {
        public DependenteRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<Dependente> BuscarPorCPF(string cpf)
        {
            return await _context.Set<Dependente>()
                 .Include(f => f.FuncionarioDependentes)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.CPF == cpf);
        }


    }
}
