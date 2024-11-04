using CGEODP.Core.DomainObjects;
using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Detran.Infra.RepositoriesRead
{
    public class VeiculoProntuarioRepositoryRead : RepositoryRead<VeiculoProntuario>, IVeiculoProntuarioRepositoryRead, IAggregateRoot
    {
        public VeiculoProntuarioRepositoryRead(ObservatorioContext context) : base(context) { }
        public async Task<VeiculoProntuario> BuscarPorCNPJ(string cnpj)
        {
            return await _context.VeiculosProntuario
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CNPJCPF == cnpj);
        }

        public async Task<VeiculoProntuario> BuscarPorCPF(string cpf)
        {
            return await _context.VeiculosProntuario
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.CNPJCPF == cpf);
        }
    }

}
