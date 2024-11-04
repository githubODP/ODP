using CGEODP.Core.DomainObjects;
using Domain.Corregedoria.Entidade;
using Domain.Corregedoria.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Corregedoria.RepositoriesRead
{
    public class InstauracaoRepositoryRead : RepositoryRead<Instauracao>, IInstauracaoRepositoryRead, IAggregateRoot
    {
        public InstauracaoRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<Instauracao> BuscarPorCNPJ(string cnpj)
        {
            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<Instauracao>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.CNPJCPF == cnpj);
        }

        public async Task<Instauracao> BuscarPorCPF(string cpf)
        {
            return await _context.Set<Instauracao>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.CNPJCPF == cpf);
        }
    }
}
