using Domain.RecursosHumanos.Entidades;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.RecursosHumanos.RepositoriesRead
{
    public class OcorrenciaRepositoryRead : RepositoryRead<Ocorrencia>, IOcorrenciaRepositoryRead
    {
        public OcorrenciaRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<Ocorrencia> BuscarPorCPF(string cpf)
        {
            return await _context.Set<Ocorrencia>()
                 .Include(f => f.FuncionarioOcorrencias)
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.CPF == cpf);
        }
    }
}
