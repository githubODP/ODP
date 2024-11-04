

using Domain.RecursosHumanos.Entidades;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.RecursosHumanos.RepositoriesRead
{
    public class FuncionarioRepositoryRead : RepositoryRead<Funcionario>, IFuncionarioRepositoryRead
    {
        public FuncionarioRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<Funcionario> BuscarPorCPF(string cpf)
        {
            return await _context.Set<Funcionario>()
                .Include(f => f.FuncionarioRubricas)
                .Include(f => f.FuncionarioDependentes)
                .Include(f => f.FuncionarioOcorrencias)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CPF == cpf);
        }




    }
}
