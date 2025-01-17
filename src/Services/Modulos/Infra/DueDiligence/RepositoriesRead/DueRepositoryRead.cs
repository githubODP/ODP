


using CGEODP.Core.DomainObjects;
using Domain.Corregedoria.Entidade;
using Domain.Corregedoria.Enum;
using Domain.DueDiligence.Entidade;
using Domain.DueDiligence.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<PagedResult<Comissionado>> ListarCOmFiltroAsync(int pageNumber, int pageSize, string orgao = null, string CPF = null)
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

                if (!string.IsNullOrEmpty(orgao))
                {
                    query = query.Where(i => i.Orgao == orgao);
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


    }
}
    


