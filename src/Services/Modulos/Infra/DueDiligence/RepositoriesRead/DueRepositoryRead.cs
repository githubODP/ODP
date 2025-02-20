
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

        public async Task<PagedResult<Comissionado>> Listar(int pageNumber, int pageSize, string termo = null)
        {
            // Cria a consulta base
            var query = _context.Set<Comissionado>().AsQueryable();

            // Aplicando filtro caso o termo seja informado
            if (!string.IsNullOrEmpty(termo))
            {
                query = query.Where(i =>
                    i.Nome.Contains(termo) ||
                    i.CPF.Contains(termo) ||
                    i.NroProtocolo.Contains(termo));
            }

            // Contando registros filtrados
            var totalRecords = await query.CountAsync();

            // Aplicando paginação
            var items = await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Calculando total de páginas
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            // Retornando resultado paginado
            return new PagedResult<Comissionado>
            {
                Results = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }

        public async Task<List<Comissionado>> ObterPorProtocolo(string nroProtocolo)
        {
            return await _context.Comissionados
                .Where(c => c.NroProtocolo == nroProtocolo)
                .Select(c => new Comissionado
                {
                    NroProtocolo = c.NroProtocolo,
                    Nome = c.Nome,
                    CPF = c.CPF,
                    Orgao = c.Orgao,
                    Observacao = c.Observacao,
                    Id = c.Id // Apenas os campos necessários
                })
                .ToListAsync(); // Retorna uma lista
        }


    }
}



