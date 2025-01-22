using Domain.Fazenda.Entidades;
using Domain.Fazenda.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Fazenda.RepositoriesRead
{
    public class NFEletronicaFederalRepositoryRead : RepositoryRead<NFEletronicaFederal>, INFEletronicaFederalRepositoryRead
    {
        public NFEletronicaFederalRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<NFEletronicaFederal>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<NFEletronicaFederal>()
                .AsNoTracking()
                .Where(c => c.CNPJ == cnpj)
                .Select(c => new NFEletronicaFederal
                {
                    NumeroNF = c.NumeroNF,
                    CNPJ = c.CNPJ,
                    Emitente = c.Emitente,
                    Destinatario = c.Destinatario,
                    CNPJDestinatario = c.CNPJDestinatario,
                    ValorNF = c.ValorNF,
                    ChaveAcesso = c.ChaveAcesso,
                })
                .Take(5)
                .ToListAsync();

        }

        public async Task<List<NFEletronicaFederal>> BuscarCPF(string cpf)
        {

            return await _context.Set<NFEletronicaFederal>()
                .AsNoTracking()
                .Where(c => c.CPF == cpf)
                 .Select(c => new NFEletronicaFederal
                 {
                     NumeroNF = c.NumeroNF,
                     CNPJ = c.CNPJ,
                     Emitente = c.Emitente,
                     Destinatario = c.Destinatario,
                     CPF = c.CPF,
                     ValorNF = c.ValorNF,
                     ChaveAcesso = c.ChaveAcesso,
                 })
                .ToListAsync();

        }

    }
}

