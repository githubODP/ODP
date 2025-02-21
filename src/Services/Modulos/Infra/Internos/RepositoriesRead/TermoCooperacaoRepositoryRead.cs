using CGEODP.Core.DomainObjects;
using Domain.DueDiligence.Entidade;
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

        public async Task<PagedResult<TermoCooperacao>> Listar(
            int pageNumber, 
            int pageSize, 
            string termo = null
            
                                                                                    )
        {
            //IQueryable<TermoCooperacao> query = _context.TermosCooperacao;

            var query = _context.Set<TermoCooperacao>().AsQueryable();



            // Aplica o filtro pelo Termo, se informado
            if (!string.IsNullOrEmpty(termo))
            {
               
                
                    query = query.Where(i =>
                        i.Orgao.Contains(termo) ||
                        i.Objeto.Contains(termo) ||
                        i.Protocolo.Contains(termo)||
                        i.Observacao.Contains(termo)||
                        i.NroTermo.Contains(termo)||
                        i.Informacoes.Contains(termo)||
                        i.Regulamentacao.Contains(termo));
                
            }

            // Aplicando paginação
            var items = await query
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            // Contando registros filtrados
            var totalRecords = await query.CountAsync();


            // Calculando total de páginas
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            // Retornando resultado paginado
            return new PagedResult<TermoCooperacao>
            {
                Results = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };
        }





        public async Task<List<TermoCooperacao>> ListarEnvio()
        {
            var diasParaAviso = new int[] { 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 };
            var dataAtual = DateTime.Today;

            var termos = await _context.TermosCooperacao
                .Where(t => t.FimVigencia > dataAtual &&
                    diasParaAviso.Contains((t.FimVigencia - dataAtual).Days))
                .OrderBy(t => t.FimVigencia)
                .ToListAsync();

            return termos;
        }

        public async Task<List<string>> EnviarAlertasPorEmail()
        {
            var termos = await ListarEnvio(); // Reutiliza o método ListarEnvio

            if (!termos.Any())
                return new List<string>(); // Retorna lista vazia caso não haja alertas

            var mensagens = termos.Select(termo =>
                $"O Termo de Cooperação {termo.NroTermo} com protocolo {termo.Protocolo} " +
                $"está próximo do vencimento. Período: {termo.InicioVigencia:dd/MM/yyyy} - " +
                $"{termo.FimVigencia:dd/MM/yyyy}."
            ).ToList();

            return mensagens; // Retorna as mensagens para serem enviadas na controller
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
