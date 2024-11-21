using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.GovernoFederal.Repositories
{
    public class AcordoLenienciaRepositoryRead : RepositoryRead<AcordoLeniencia>, IAcordoLenienciaRepositoryRead
    {
        public AcordoLenienciaRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<AcordoLeniencia>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<AcordoLeniencia>()
                .AsNoTracking()
                .Where(c => c.CNPJ == cnpj)
                .Select(c => new AcordoLeniencia
                {
                    IdentificacaoAcordo = c.IdentificacaoAcordo,
                    NumeroProcesso = c.NumeroProcesso,
                    CNPJ = c.CNPJ,
                    RazaoSocial = c.RazaoSocial,
                    DataInicioAcordo = c.DataInicioAcordo,
                    DataFimAcordo = c.DataFimAcordo,
                    Situacao = c.Situacao,
                    Efeitos = c.Efeitos,
                    TermosAcordo = c.TermosAcordo,
                })
                .ToListAsync();

        }

        public Task<List<AcordoLeniencia>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
