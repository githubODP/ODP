using Domain.Tribunal.Entidades.TSE;
using Domain.Tribunal.Interfaces.TSE;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Tribunal.Repository.TSE
{
    public class FornecedorPartidoRepositoryRead : RepositoryRead<FornecedorPartido>, IFornecedorPartidoRepositoryRead
    {
        public FornecedorPartidoRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<FornecedorPartido>> BuscarCNPJ(string cnpj)
        {
            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<FornecedorPartido>()
                                 .AsNoTracking()
                                 .Where(c => c.CNPJCPF == cnpj)
            .Select(c => new FornecedorPartido
            {
               AnoEleicao = c.AnoEleicao,
               Municipio = c.Municipio,
               UF = c.UF,
               Partido = c.Partido,
               Fornecedor = c.Fornecedor,
               CNPJCPF = c.CNPJCPF,
               DescricaoDespesas = c.DescricaoDespesas, 
               PartidoFornecedor = c.PartidoFornecedor, 
               ValorDespesas = c.ValorDespesas,
            })
            .ToListAsync();
        }

        public async Task<List<FornecedorPartido>> BuscarCPF(string cpf)
        {
            return await _context.Set<FornecedorPartido>()
                                 .AsNoTracking()
                                 .Where(c => c.CNPJCPF == cpf)
            .Select(c => new FornecedorPartido
            {
                AnoEleicao = c.AnoEleicao,
                Municipio = c.Municipio,
                UF = c.UF,
                Partido = c.Partido,
                Fornecedor = c.Fornecedor,
                CNPJCPF = c.CNPJCPF,
                DescricaoDespesas = c.DescricaoDespesas,
                PartidoFornecedor = c.PartidoFornecedor,
                ValorDespesas = c.ValorDespesas,
            })
            .ToListAsync();
        }
    }
}
