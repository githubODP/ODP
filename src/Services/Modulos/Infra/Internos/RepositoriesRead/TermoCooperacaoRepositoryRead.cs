using CGEODP.Core.DomainObjects;
using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.Internos.RepositoriesRead
{
    public class TermoCooperacaoRepositoryRead : RepositoryRead<TermoCooperacao>, ITermoCooperacaoRepositoryRead, IAggregateRoot
    {
        public TermoCooperacaoRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<PagedResult<TermoCooperacao>> ListarComFiltroAsync(int pageNumber, int pageSize, string protocolo = null)
        {
            IQueryable<TermoCooperacao> query = _context.TermosCooperacao;

            // Aplica o filtro pelo Protocolo, se informado
            if (!string.IsNullOrEmpty(protocolo))
            {
                query = query.Where(t => t.Protocolo.Contains(protocolo));
            }

            // Conta o total de registros após o filtro
            int totalRecords = await query.CountAsync();

            // Paginação dos dados
            var items = await query
                .OrderBy(t => t.Protocolo) // Ordenação opcional
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Retorna o resultado paginado
            return new PagedResult<TermoCooperacao>
            {
                Results = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }





        public async Task<List<TermoCooperacao>> ListarEnvio()
        {
            var diasParaAviso = new int[] { 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 }; // Intervalos de alerta

            var termos = await _context.TermosCooperacao
                .Where(t => diasParaAviso.Contains(EF.Functions.DateDiffDay(t.InicioVigencia, t.FimVigencia)))
                .OrderBy(t => t.FimVigencia)
                .ToListAsync();

            return termos;
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
                     FimVigencia = c.FimVigencia,
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
