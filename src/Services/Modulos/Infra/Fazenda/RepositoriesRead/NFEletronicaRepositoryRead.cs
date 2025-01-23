using Domain.Fazenda.Entidades;
using Domain.Fazenda.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Fazenda.RepositoriesRead
{
    public class NFEletronicaRepositoryRead : RepositoryRead<NFEletronica>, INFEletronicaRepositoryRead
    {
        public NFEletronicaRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<NFEletronica>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<NFEletronica>()
                .AsNoTracking()
                .Where(c => c.CNPJ == cnpj)
                .Select(c => new NFEletronica
                {
                    Emitente = c.Emitente,
                    CNPJ = c.CNPJ,
                    NumeroNF = c.NumeroNF,
                    Destinatario = c.Destinatario,
                    CNPJDestinatario = c.CNPJDestinatario,
                    ValorNotaFiscal = c.ValorNotaFiscal,
                    DataEmissao = c.DataEmissao,
                    Ano = c.Ano,
                })
                .Take(5)
                .ToListAsync();

        }

        public Task<List<NFEletronica>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
