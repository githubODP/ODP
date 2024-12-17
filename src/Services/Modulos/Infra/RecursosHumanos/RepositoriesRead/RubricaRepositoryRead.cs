using Domain.RecursosHumanos.Entidades;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.RecursosHumanos.RepositoriesRead
{
    public class RubricaRepositoryRead : RepositoryRead<Rubrica>, IRubricaRepositoryRead
    {
        public RubricaRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<Rubrica> BuscarPorCPF(string cpf)
        {
            return await _context.Set<Rubrica>()

                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.CPF == cpf);
        }
    }
}

