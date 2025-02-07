using CGEODP.Core.DomainObjects;
using Domain.Corregedoria.Entidade;
using Domain.DueDiligence.Entidade;
using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infra.Internos.RepositoriesRead
{
    public class TermoCooperacaoRepositoryRead : RepositoryRead<TermoCooperacao>, ITermoCooperacaoRepositoryRead, IAggregateRoot
    {
        public TermoCooperacaoRepositoryRead(ObservatorioContext context) : base(context) { }




        public async Task<PagedResult<TermoCooperacao>> ListarComFiltrosAsync(int pageNumber, int pageSize)
        {
            var query = _context.Set<TermoCooperacao>().AsQueryable();

            var totalRecords = await query.CountAsync();

            var items = await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new PagedResult<TermoCooperacao>
            {
                Results = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        
        }

        public async Task<TermoCooperacao> ObterProtocolo(string protocolo)
        {
            return await _context.TermosCooperacao
                 .Where(c => c.Protocolo == protocolo)
                 .Select(c => new TermoCooperacao
                 {
                     Protocolo = c.Protocolo,
                     Orgao = c.Orgao,
                     Sigla = c.Sigla,
                     NroTermo = c.NroTermo,
                     InicioVigencia = c.InicioVigencia,
                     FimVIgencia = c.FimVIgencia,
                     Validade = c.Validade,
                     Ativo = c.Ativo,
                     Status = c.Status,
                     Renovar = c.Renovar,
                     DIOE = c.DIOE,
                     DataPublicacao = c.DataPublicacao,
                     Objeto = c.Objeto,
                     Regulamentacao = c.Regulamentacao,
                     Informacoes = c.Informacoes,
                     Observacao = c.Observacao
                 }).FirstOrDefaultAsync(); 
        }



    }
}
