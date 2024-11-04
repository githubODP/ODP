using CGEODP.Core.DomainObjects;
using Domain.Compras.Entidades;
using Domain.Compras.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Compras.RepositoriesRead
{
    public class LicitacaoRepositoryRead : RepositoryRead<Licitacao>, ILicitacaoRepositoryRead, IAggregateRoot
    {
        public LicitacaoRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<List<Licitacao>> BuscarCNPJ(string cnpj)
        {
            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<Licitacao>()
            .AsNoTracking()
            .Where(c => c.CNPJCPF == cnpj)
            .Select ( c => new Licitacao
            {
                Ano = c.Ano,
                Orgao = c.Orgao,
                CNPJCPF = c.CNPJCPF,
                Protocolo = c.Protocolo,
                ValorHomologado = c.ValorHomologado,    
            })
            .ToListAsync();
        }

        public Task<List<Licitacao>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}

