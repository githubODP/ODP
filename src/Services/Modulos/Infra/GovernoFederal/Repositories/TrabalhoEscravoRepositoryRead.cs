using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.GovernoFederal.Repositories
{
    public class TrabalhoEscravoRepositoryRead : RepositoryRead<TrabalhoEscravo>, ITrabalhoEscravoRepositoryRead
    {
        public TrabalhoEscravoRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<TrabalhoEscravo>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<TrabalhoEscravo>()
                .AsNoTracking()
                .Where(c => c.CNPJCPF == cnpj)
                .Select(c => new TrabalhoEscravo
                {
                    Empregador = c.Empregador,
                    CNPJCPF = c.CNPJCPF,
                    Estabelecimento = c.Estabelecimento,
                    Ano = c.Ano,
                    UF = c.UF,
                })
                .ToListAsync();
        }

        public async Task<List<TrabalhoEscravo>> BuscarCPF(string cpf)
        {

            return await _context.Set<TrabalhoEscravo>()
                .AsNoTracking()
                .Where(c => c.CNPJCPF == cpf)
                .Select(c => new TrabalhoEscravo
                {
                    Empregador = c.Empregador,
                    CNPJCPF = c.CNPJCPF,
                    Estabelecimento = c.Estabelecimento,
                    Ano = c.Ano,
                    UF = c.UF,
                })
                .ToListAsync();

        }

    }
}
