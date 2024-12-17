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
                .Where(c => c.CNPJ == cnpj)
                .Select(c => new Licitacao
                {
                    Ano = c.Ano,
                    Orgao = c.Orgao ?? string.Empty,
                    Fornecedor = c.Fornecedor ?? string.Empty,
                    CNPJ = c.CNPJ ?? string.Empty,
                    Protocolo = c.Protocolo ?? string.Empty,
                    ValorHomologado = c.ValorHomologado ?? 0,
                })
                .OrderByDescending(c => c.Ano)
                .Take(5)
                .ToListAsync(); ;
        }

        public Task<List<Licitacao>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}

