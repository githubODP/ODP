using CGEODP.Core.DomainObjects;
using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Detran.Infra.RepositoriesRead
{
    public class VeiculoBaixadoRepositoryRead : RepositoryRead<VeiculoBaixado>, IVeiculoBaixadoRepositoryRead, IAggregateRoot
    {
        public VeiculoBaixadoRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<VeiculoBaixado> BuscarPorCNPJ(string cnpj)
        {
            return await _context.VeiculosBaixados
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CNPJCPF == cnpj);
        }

        public async Task<VeiculoBaixado> BuscarPorCPF(string cpf)
        {
            return await _context.VeiculosBaixados
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.CNPJCPF == cpf);
        }
    }

}
