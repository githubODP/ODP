using Domain.Tribunal.Entidades.TCE;
using Domain.Tribunal.Interfaces.TCE;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;


namespace Infra.Tribunal.Repository.TCE
{
    public class CNPJRestritoRepositoryRead : RepositoryRead<CNPJRestrito>, ICNPJRestritoRepositoryRead
    {
        public CNPJRestritoRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<CNPJRestrito>> BuscarCNPJ(string cnpj)
        {
            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<CNPJRestrito>()
                                 .AsNoTracking()
                                 .Where(c => c.CNPJ == cnpj)
            .Select(c => new CNPJRestrito
            {
                NomeRazaoSocial = c.NomeRazaoSocial,
                CNPJ = c.CNPJ,
                DataInicio = c.DataInicio,
                DataFim = c.DataFim,
                TipoSancao = c.TipoSancao,
                Situacao = c.Situacao,


            })
            .ToListAsync();
        }

        public Task<List<CNPJRestrito>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }


}
