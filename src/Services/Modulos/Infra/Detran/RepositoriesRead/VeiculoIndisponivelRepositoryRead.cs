using CGEODP.Core.DomainObjects;
using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
namespace Detran.Infra.RepositoriesRead
{
    public class VeiculoIndisponivelRepositoryRead : RepositoryRead<VeiculoIndispAdmin>, IVeiculoIndisponivelRepositoryRead, IAggregateRoot
    {
        public VeiculoIndisponivelRepositoryRead(ObservatorioContext context) : base(context) { }
        public async Task<VeiculoIndispAdmin> BuscarPorCNPJ(string cnpj)
        {
            return await _context.VeiculosIndisponivel
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CNPJCPF == cnpj);
        }

        public async Task<VeiculoIndispAdmin> BuscarPorCPF(string cpf)
        {
            return await _context.VeiculosIndisponivel
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.CNPJCPF == cpf);
        }

    }
}
