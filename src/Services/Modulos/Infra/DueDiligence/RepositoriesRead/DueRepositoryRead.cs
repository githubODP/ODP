
using CGEODP.Core.DomainObjects;
using Domain.DueDiligence.Entidade;
using Domain.DueDiligence.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.DueDiligence.RepositoriesRead
{
    public class DueRepositoryRead : RepositoryRead<Comissionado>, IDueRepositoryRead, IAggregateRoot
    {
        public DueRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<List<Comissionado>> BuscarPorCPF(string cpf)
        {
            return await _context.Set<Comissionado>()
                .AsNoTracking()
                .Where(c => c.CPF == cpf)
                .Select(c => new Comissionado
                {
                    Nome = c.Nome,
                    CPF = c.CPF,
                    Orgao = c.Orgao,
                    DataResposta = c.DataResposta,
                    Observacao = c.Observacao,
                })
                .ToListAsync();
        }

        public async Task<PagedResult<Comissionado>> ListarCOmFiltroAsync(int pageNumber, int pageSize, string nome = null, string CPF = null, string protocolo = null)
        {
            // Cria a consulta base
            var query = _context.Set<Comissionado>().AsQueryable();

            // Prioriza a busca pelo protocolo, se informado
            if (!string.IsNullOrEmpty(CPF))
            {
                query = query.Where(i => i.CPF.Contains(CPF));
            }
            else
            {

                if (!string.IsNullOrEmpty(nome))
                {
                    query = query.Where(i => i.Nome == nome);
                }

                if (!string.IsNullOrEmpty(protocolo))
                {
                    query = query.Where(i => i.NroProtocolo == protocolo);
                }
            }

            // Conta o número total de registros após os filtros
            var totalRecords = await query.CountAsync();

            // Aplica paginação
            var items = await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Calcula o número total de páginas
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            // Retorna os resultados no formato PagedResult
            return new PagedResult<Comissionado>
            {
                Results = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }

        public async Task<Comissionado> ObterPorProtocolo(string nroProtocolo)
        {
            return await _context.Comissionados
                .Where(c => c.NroProtocolo == nroProtocolo)
                .Select(c => new Comissionado
                {
                    NroProtocolo = c.NroProtocolo,
                    Nome = c.Nome,
                    CPF = c.CPF,
                    Orgao = c.Orgao,
                    Observacao = c.Observacao // Apenas os campos necessários
                })
                .FirstOrDefaultAsync();
        }


    }
}
    


