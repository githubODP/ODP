using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Infra.Internos.Repositories
{
    public class TermoCooperacaoRepository : Repository<TermoCooperacao>, ITermoCooperacaoRepository
    {
        public TermoCooperacaoRepository(ObservatorioContext context) : base(context) { }


        public override async Task Atualizar(TermoCooperacao termo)
        {
            var termoExistente = await _context.Set<TermoCooperacao>().FindAsync(termo.Id);
            if (termoExistente == null)
            {
                throw new InvalidOperationException("Termo de cooperação não encontrado.");
            }

            _context.Entry(termoExistente).CurrentValues.SetValues(termo);
            await _context.SaveChangesAsync();
        }
    }
}
