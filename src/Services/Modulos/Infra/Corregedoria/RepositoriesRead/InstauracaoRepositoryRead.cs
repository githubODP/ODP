using CGEODP.Core.DomainObjects;
using Domain.Corregedoria.Entidade;
using Domain.Corregedoria.Enum;
using Domain.Corregedoria.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Web;

namespace Infra.Corregedoria.RepositoriesRead
{
    public class InstauracaoRepositoryRead : RepositoryRead<Instauracao>, IInstauracaoRepositoryRead, IAggregateRoot
    {
        private readonly ILogger<InstauracaoRepositoryRead> _logger;
        public InstauracaoRepositoryRead(ObservatorioContext context, ILogger<InstauracaoRepositoryRead> logger) : base(context)
        {
            _logger = logger;

        }

        public async Task<Instauracao> BuscarPorCNPJ(string cnpj)
        {
            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<Instauracao>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.CNPJCPF == cnpj);
        }

        public async Task<Instauracao> BuscarPorCPF(string cpf)
        {
            return await _context.Set<Instauracao>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.CNPJCPF == cpf);
        }

        public async Task<PagedResult<Instauracao>> ListarComFiltrosAsync(
                int pageNumber,
                int pageSize,
                int? ano = null,
                string orgao = null,
                string procedimento = null,
                string decisao = null,
                string protocolo = null) // Protocolo adicionado
        {

            _logger.LogInformation("Iniciando ListarComFiltrosAsync no repositório com parâmetros: pageNumber={PageNumber}, pageSize={PageSize}, ano={Ano}, orgao={Orgao}, procedimento={Procedimento}, decisao={Decisao}, protocolo={Protocolo}", pageNumber, pageSize, ano, orgao, procedimento, decisao, protocolo);
            // Cria a consulta base
            var query = _context.Set<Instauracao>().AsQueryable();

            // Prioriza a busca pelo protocolo, se informado
            if (!string.IsNullOrEmpty(protocolo))
            {
                query = query.Where(i => i.Protocolo.Contains(protocolo));
            }
            else
            {
                // Aplica filtros opcionais se protocolo não foi fornecido
                if (ano.HasValue)
                {
                    query = query.Where(i => i.Ano == ano.Value);
                }

                if (!string.IsNullOrEmpty(orgao))
                {
                    if (Enum.TryParse<ETipoOrgao>(orgao, out var orgaoEnum))
                    {
                        query = query.Where(i => i.Orgao == orgaoEnum);
                    }
                }

                if (!string.IsNullOrEmpty(procedimento))
                {
                    if (Enum.TryParse<ETipoProcedimento>(procedimento, out var procedimentoEnum))
                    {
                        query = query.Where(i => i.Procedimento == procedimentoEnum);
                    }
                }

                if (!string.IsNullOrEmpty(decisao))
                {
                    if (Enum.TryParse<ETipoDecisao>(decisao, out var decisaoEnum))
                    {
                        query = query.Where(i => i.Decisao == decisaoEnum);
                    }
                }
            }

            try
            {
                var totalRecords = await query.CountAsync();
                _logger.LogDebug("Consulta retornou {TotalRecords} registros após filtros.", totalRecords);

                var items = await query
                    .AsNoTracking()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                _logger.LogInformation("Consulta retornou {Count} registros paginados.", items.Count);

                return new PagedResult<Instauracao>
                {
                    Results = items,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = totalRecords,
                    TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao executar ListarComFiltrosAsync no repositório.");
                throw;
            }
        }


    }
}
