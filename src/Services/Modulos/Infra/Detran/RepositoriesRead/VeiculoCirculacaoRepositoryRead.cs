using CGEODP.Core.DomainObjects;
using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Detran.Infra.RepositoriesRead
{
    public class VeiculoCirculacaoRepositoryRead : RepositoryRead<VeiculoCirculacao>, IVeiculoCirculacaoRepositoryRead, IAggregateRoot
    {
        public VeiculoCirculacaoRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<VeiculoCirculacao> BuscarPorCNPJ(string cnpj)
        {
            return await _context.VeiculosCirculacao
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CNPJCPF == cnpj);
        }

        public async Task<VeiculoCirculacao> BuscarPorCPF(string cpf)
        {
            return await _context.VeiculosCirculacao
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.CNPJCPF == cpf);
        }

        public Task<List<VeiculoCirculacao>> CNPJCPFAvancada(string cnpjcpf)
        {
            var veiculos = _context.Set<VeiculoCirculacao>()
                 .Where(c => c.CNPJCPF == cnpjcpf)
                 .AsNoTracking()
                 .ToListAsync();

            return veiculos;
        }
    }
}
