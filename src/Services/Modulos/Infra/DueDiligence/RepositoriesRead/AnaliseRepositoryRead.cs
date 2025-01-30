using CGEODP.Core.DomainObjects;
using Domain.DueDiligence.Entidade;
using Domain.DueDiligence.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.DueDiligence.RepositoriesRead
{
    public class AnaliseRepositoryRead : RepositoryRead<Analise>, IAnaliseRepositoryRead, IAggregateRoot
    {
        public AnaliseRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<PagedResult<AnaliseResponseDTO>> ListarDadosAdicionais(int pageNumber, int pageSize)
        {
            // Consulta base com os dados adicionais relacionados
            var query = _context.Analises
                .Include(a => a.Comissionado) // Inclui dados relacionados
                .AsNoTracking() // Melhor para consultas somente leitura
                .Select(a => new AnaliseResponseDTO
                {
                    Id = a.Id,
                    NroProtocolo = a.NroProtocolo,
                    DataAnalise = a.DataAnalise,
                    AnaliseTecnica = a.AnaliseTecnica,
                    Risco = a.Risco.ToString(), // Enum convertido para string
                    Ressalvas = a.Ressalvas.ToString(), // Enum convertido para string
                    Responsavel = a.Responsavel,
                    Nome = a.Comissionado != null ? a.Comissionado.Nome : null,
                    CPF = a.Comissionado != null ? a.Comissionado.CPF : null,
                    Orgao = a.Comissionado != null ? a.Comissionado.Orgao : null,
                    Observacao = a.Comissionado != null ? a.Comissionado.Observacao : null
                })
                .AsQueryable();

            // Total de registros para paginação
            var totalRecords = await query.CountAsync();

            // Aplicar paginação
            var results = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Retorno paginado
            return new PagedResult<AnaliseResponseDTO>
            {
                Results = results,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
            };
        }

    }
}
