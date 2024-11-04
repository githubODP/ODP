using Dividas.Domain.Entidades;
using Dividas.Domain.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;


namespace Dividas.Infra.Repositories
{
    public class DividaFgtsRepositoryRead : RepositoryRead<DividaFGTS>, IDividaFgtsRepositoryRead
    {
        public DividaFgtsRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<DividaFGTS>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<DividaFGTS>()
                .AsNoTracking()
                .Where(c => c.CNPJCPF == cnpj)
                .Select (c => new DividaFGTS
                {
                    TipoPessoa = c.TipoPessoa,
                    NomeDevedor = c.NomeDevedor,
                    CNPJCPF = c.CNPJCPF,
                    UFUnidadeResponsavel = c.UFUnidadeResponsavel,  
                    ValorConsolidado = c.ValorConsolidado,  
                })
                .ToListAsync();

        }

        public async Task<List<DividaFGTS>> BuscarCPF(string cpf)
        {

            return await _context.Set<DividaFGTS>()
                .AsNoTracking()
                .Where(c => c.CNPJCPF == cpf)
                 .Select(c => new DividaFGTS
                 {
                     TipoPessoa = c.TipoPessoa,
                     NomeDevedor = c.NomeDevedor,
                     CNPJCPF = c.CNPJCPF,
                     UFUnidadeResponsavel = c.UFUnidadeResponsavel,
                     ValorConsolidado = c.ValorConsolidado,
                 })
                .ToListAsync();

        }








    }
}
