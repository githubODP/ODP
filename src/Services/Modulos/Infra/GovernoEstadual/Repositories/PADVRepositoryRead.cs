using CGEODP.Core.DomainObjects;
using Domain.GovernoEstadual.Entidades;
using Domain.GovernoEstadual.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.GovernoEstadual.Repositories
{

    public class PADVRepositoryRead : RepositoryRead<PADV>, IPADVRepositoryRead, IAggregateRoot
    {
        public PADVRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<List<PADV>> BuscarCNPJ(string cnpj)
        {
            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<PADV>()
                .AsNoTracking()
                .Where(c => c.CNPJExecutor == cnpj)
                .Select(c => new PADV
                {
                    RazaoSocial = c.RazaoSocial,
                    Agencia = c.Agencia,
                    CNPJExecutor = c.CNPJExecutor,
                    NumeroPADV = c.NumeroPADV,
                    OrgaoPagador = c.OrgaoPagador,
                    ValorPago = c.ValorPago,
                    Ano = c.Ano,
                })
                .OrderByDescending(c => c.Ano)
                .Take(5)
                .ToListAsync();

        }

        public Task<List<PADV>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }


    }
}
