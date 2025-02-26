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
            string identificador = null)
        {
            _logger.LogInformation("Iniciando ListarComFiltrosAsync com parâmetros: pageNumber={PageNumber}, pageSize={PageSize}, ano={Ano}, orgao={Orgao}, procedimento={Procedimento}, decisao={Decisao}, identificador={Identificador}", pageNumber, pageSize, ano, orgao, procedimento, decisao, identificador);

            var query = _context.Set<Instauracao>().AsQueryable();

            if (!string.IsNullOrEmpty(identificador))
            {
                if (identificador.Length == 12)
                {
                    // Considera como protocolo
                    query = query.Where(i => i.Protocolo.Contains(identificador));
                }
                else
                {
                    // Considera como CNPJ/CPF
                    query = query.Where(i => i.CNPJCPF.Contains(identificador));
                }
            }
            else
            {
                if (ano.HasValue)
                {
                    query = query.Where(i => i.Ano == ano.Value);
                }

                if (!string.IsNullOrEmpty(orgao) && Enum.TryParse<ETipoOrgao>(orgao, out var orgaoEnum))
                {
                    query = query.Where(i => i.Orgao == orgaoEnum);
                }

                if (!string.IsNullOrEmpty(procedimento) && Enum.TryParse<ETipoProcedimento>(procedimento, out var procedimentoEnum))
                {
                    query = query.Where(i => i.Procedimento == procedimentoEnum);
                }

                if (!string.IsNullOrEmpty(decisao) && Enum.TryParse<ETipoDecisao>(decisao, out var decisaoEnum))
                {
                    query = query.Where(i => i.Decisao == decisaoEnum);
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
                _logger.LogError(ex, "Erro ao executar ListarComFiltrosAsync.");
                throw;
            }
        }


        private bool IsValidCpfCnpj(string value)
        {
            // Remover caracteres não numéricos
            var numericValue = new string(value.Where(char.IsDigit).ToArray());

            if (numericValue.Length == 11)
            {
                // Validar CPF
                return IsValidCpf(numericValue);
            }
            else if (numericValue.Length == 14)
            {
                // Validar CNPJ
                return IsValidCnpj(numericValue);
            }

            return false;
        }

        private bool IsValidCpf(string cpf)
        {
            // Implementar a validação de CPF conforme as regras específicas
            // Você pode encontrar algoritmos de validação de CPF em fontes confiáveis
            // Exemplo: https://www.macoratti.net/11/09/c_val1.htm
            // Certifique-se de adaptar o código para remover caracteres não numéricos antes da validação
            // e de que o CPF possui exatamente 11 dígitos
            return true; // Substitua pelo resultado da validação
        }

        private bool IsValidCnpj(string cnpj)
        {
            // Implementar a validação de CNPJ conforme as regras específicas
            // Você pode encontrar algoritmos de validação de CNPJ em fontes confiáveis
            // Exemplo: https://www.macoratti.net/11/09/c_val1.htm
            // Certifique-se de adaptar o código para remover caracteres não numéricos antes da validação
            // e de que o CNPJ possui exatamente 14 dígitos
            return true; // Substitua pelo resultado da validação
        }




    }
}
