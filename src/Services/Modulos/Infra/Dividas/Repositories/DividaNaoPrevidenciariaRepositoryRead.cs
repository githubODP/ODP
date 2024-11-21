using Dividas.Domain.Entidades;
using Dividas.Domain.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Dividas.Infra.Repositories
{
    public class DividaNaoPrevidenciariaRepositoryRead : RepositoryRead<DividaNaoPrevidenciaria>, IDividaNaoPrevidenciaRepositoryRead
    {
        public DividaNaoPrevidenciariaRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<DividaNaoPrevidenciaria>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<DividaNaoPrevidenciaria>()
                .AsNoTracking()
                .Where(c => c.CNPJ == cnpj)
                .Select(c => new DividaNaoPrevidenciaria
                {
                    TipoPessoa = c.TipoPessoa,
                    NomeDevedor = c.NomeDevedor,
                    CNPJ = c.CNPJ,
                    DataInscricao = c.DataInscricao,
                    ValorConsolidado = c.ValorConsolidado,
                })
                .ToListAsync();

        }

        public async Task<List<DividaNaoPrevidenciaria>> BuscarCPF(string cpf)
        {

            return await _context.Set<DividaNaoPrevidenciaria>()
                .AsNoTracking()
                .Where(c => c.CPF == cpf)
                 .Select(c => new DividaNaoPrevidenciaria
                 {
                     TipoPessoa = c.TipoPessoa,
                     NomeDevedor = c.NomeDevedor,
                     CPF = c.CPF,
                     DataInscricao = c.DataInscricao,
                     ValorConsolidado = c.ValorConsolidado,
                 })
                .ToListAsync();

        }

    }
}
