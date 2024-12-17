using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.GovernoFederal.Repositories
{
    public class CnepRepositoryRead : RepositoryRead<Cnep>, ICnepRepositoryRead
    {
        public CnepRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<Cnep>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<Cnep>()
                .AsNoTracking()
                .Where(c => c.CNPJ == cnpj)
                .Select(c => new Cnep
                {
                    RazaoSocial = c.RazaoSocial,
                    CNPJ = c.CNPJ,
                    NroProcesso = c.NroProcesso,
                    DataInicioSancao = c.DataInicioSancao,
                    DataFimSancao = c.DataFimSancao,
                    ValorMulta = c.ValorMulta,
                    FundamentacaoLegal = c.FundamentacaoLegal,

                })
                .ToListAsync();

        }

        public async Task<List<Cnep>> BuscarCPF(string cpf)
        {
            return await _context.Set<Cnep>()
               .AsNoTracking()
               .Where(c => c.CPF == cpf)
               .Select(c => new Cnep
               {
                   RazaoSocial = c.RazaoSocial,
                   CPF = c.CPF,
                   NroProcesso = c.NroProcesso,
                   DataInicioSancao = c.DataInicioSancao,
                   DataFimSancao = c.DataFimSancao,
                   ValorMulta = c.ValorMulta,
                   FundamentacaoLegal = c.FundamentacaoLegal,

               })
               .ToListAsync();
        }
    }
}
