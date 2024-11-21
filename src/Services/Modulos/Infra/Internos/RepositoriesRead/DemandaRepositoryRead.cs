using CGEODP.Core.DomainObjects;
using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Web;

namespace Infra.Internos.RepositoriesRead
{
    public class DemandaRepositoryRead : RepositoryRead<DemandasInternas>, IDemandaRepositoryRead, IAggregateRoot
    {
        public DemandaRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<List<DemandasInternas>> BuscarCNPJ(string cnpj)
        {
            // Decodifica o CNPJ caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            // Normaliza o CNPJ pesquisado, removendo pontos, traços e barras
            var consultacnpj = RemoverCaracteresEspeciais(cnpj);

            return await _context.Set<DemandasInternas>()
                                 .AsNoTracking()
                                 .Where(d => RemoverCaracteresEspeciais(d.Observacao).Contains(consultacnpj))
                                 .Select(d => new DemandasInternas
                                 {
                                     Ano = d.Ano,
                                     Mes = d.Mes,
                                     Protocolo = d.Protocolo,
                                     Objeto = d.Objeto,
                                     Orgao = d.Orgao,
                                     Solicitante = d.Solicitante,
                                     NomeSolicita = d.NomeSolicita,
                                     Observacao = d.Observacao,

                                 })
                                 .ToListAsync();
        }


        public async Task<List<DemandasInternas>> BuscarCPF(string cpf)
        {


            // Normaliza o documento de busca, removendo pontos, traços e barras
            var consultacpf = RemoverCaracteresEspeciais(cpf);

            return await _context.Set<DemandasInternas>()
                                 .AsNoTracking()
                                 .Where(d => RemoverCaracteresEspeciais(d.Observacao).Contains(consultacpf))
                                 .Select(d => new DemandasInternas
                                 {
                                     Ano = d.Ano,
                                     Mes = d.Mes,
                                     Protocolo = d.Protocolo,
                                     Objeto = d.Objeto,
                                     Orgao = d.Orgao,
                                     Solicitante = d.Solicitante,
                                     NomeSolicita = d.NomeSolicita,
                                     Observacao = d.Observacao,

                                 })
                                 .ToListAsync();
        }

        // Método auxiliar para remover caracteres especiais
        private string RemoverCaracteresEspeciais(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;

            return Regex.Replace(valor, "[^0-9]", ""); // Mantém apenas números
        }








    }
}
