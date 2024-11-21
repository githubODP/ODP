using Domain.Fazenda.Entidades;
using Domain.Fazenda.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Fazenda.RepositoriesRead
{
    public class JuceparRepositoryRead : RepositoryRead<Jucepar>, IJuceparRepositoryRead
    {
        public JuceparRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<Jucepar>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<Jucepar>()
                .AsNoTracking()
                .Where(c => c.CNPJ == cnpj)
                .Select(c => new Jucepar
                {
                    RazaoSocial = c.RazaoSocial,
                    CNPJ = c.CNPJ,
                    NomesSocio = c.NomesSocio,
                    CPF = c.CPF,
                    Situacao = c.Situacao,
                })
                .ToListAsync();

        }

        public async Task<List<Jucepar>> BuscarCPF(string cpf)
        {

            return await _context.Set<Jucepar>()
                .AsNoTracking()
                .Where(c => c.CPF == cpf)
                 .Select(c => new Jucepar
                 {
                     RazaoSocial = c.RazaoSocial,
                     CNPJ = c.CNPJ,
                     NomesSocio = c.NomesSocio,
                     CPF = c.CPF,
                     Situacao = c.Situacao,
                 })
                .ToListAsync();

        }







    }
}
