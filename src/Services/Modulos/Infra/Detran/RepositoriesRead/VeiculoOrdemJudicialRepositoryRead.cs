using CGEODP.Core.DomainObjects;
using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Detran.Infra.RepositoriesRead
{
    public class VeiculoOrdemJudicialRepositoryRead : RepositoryRead<VeiculoOrdemJudicial>, IVeiculoOrdemJudicialRepositoryRead, IAggregateRoot
    {
        public VeiculoOrdemJudicialRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<VeiculoOrdemJudicial> BuscarPorCNPJ(string cnpj)
        {
            return await _context.VeiculosJudiciais
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CNPJCPF == cnpj);
        }

        public async Task<VeiculoOrdemJudicial> BuscarPorCPF(string cpf)
        {
            return await _context.VeiculosJudiciais
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.CNPJCPF == cpf);
        }
    }
}
