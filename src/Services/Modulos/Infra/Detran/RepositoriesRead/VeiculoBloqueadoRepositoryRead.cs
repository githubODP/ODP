using CGEODP.Core.DomainObjects;
using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Detran.Infra.RepositoriesRead
{
    public class VeiculoBloqueadoRepositoryRead : RepositoryRead<VeiculoBloqueioRoubo>, IVeiculoBloqueadoRepositoryRead, IAggregateRoot
    {
        public VeiculoBloqueadoRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<VeiculoBloqueioRoubo> BuscarPorCNPJ(string cnpj)
        {
            return await _context.VeiculosBloqueio
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CNPJCPF == cnpj);
        }

        public async Task<VeiculoBloqueioRoubo> BuscarPorCPF(string cpf)
        {
            return await _context.VeiculosBloqueio
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.CNPJCPF == cpf);
        }
    }
}
