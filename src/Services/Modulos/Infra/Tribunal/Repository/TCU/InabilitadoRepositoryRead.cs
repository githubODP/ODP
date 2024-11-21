using Domain.Tribunal.Entidades.TCU;
using Domain.Tribunal.Interfaces.TCU;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.Tribunal.Repository.TCU
{
    public class InabilitadoRepositoryRead : RepositoryRead<Inabilitado>, IInabilitadoRepositoryRead
    {
        public InabilitadoRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<Inabilitado>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Inabilitado>> BuscarCPF(string cpf)
        {
            return await _context.Set<Inabilitado>()
                                  .AsNoTracking()
                                  .Where(c => c.CPF == cpf)
            .Select(c => new Inabilitado
            {
                Nome = c.Nome,
                CPF = c.CPF,
                Processo = c.Processo,
                Deliberacao = c.Deliberacao,
                DataFinal = c.DataFinal,
            })
            .ToListAsync();
        }

    }
}
