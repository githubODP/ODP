using CGEODP.Core.DomainObjects;
using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Detran.Infra.RepositoriesRead
{
    public class VeiculoRegistroForaRepositoryRead : RepositoryRead<VeiculoRegistroFora>, IVeiculoRegistroForaRepositoryRead, IAggregateRoot
    {
        public VeiculoRegistroForaRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<VeiculoRegistroFora> BuscarPorCNPJ(string cnpj)
        {
            return await _context.VeiculosRegistradoFora
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CNPJCPF == cnpj);
        }

        public async Task<VeiculoRegistroFora> BuscarPorCPF(string cpf)
        {
            return await _context.VeiculosRegistradoFora
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.CNPJCPF == cpf);
        }
    }
}
