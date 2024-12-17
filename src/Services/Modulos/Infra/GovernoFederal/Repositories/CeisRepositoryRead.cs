using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;


namespace Infra.GovernoFederal.Repositories
{
    public class CeisRepositoryRead : RepositoryRead<Ceis>, ICeisRepositoryRead
    {
        public CeisRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<Ceis>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<Ceis>()
                .AsNoTracking()
                .Where(c => c.CNPJ == cnpj)
                .Select(c => new Ceis
                {
                    RazaoSocialReceita = c.RazaoSocialReceita,
                    CNPJ = c.CNPJ,
                    TipoPessoa = c.TipoPessoa,
                    TipoSancao = c.TipoSancao,
                    DTInicioSancao = c.DTInicioSancao,
                    DTFimSancao = c.DTFimSancao,
                })
                .ToListAsync();


        }

        public async Task<List<Ceis>> BuscarCPF(string cpf)
        {

            return await _context.Set<Ceis>()
                .AsNoTracking()
                .Where(c => c.CPF == cpf)
                 .Select(c => new Ceis
                 {
                     RazaoSocialReceita = c.RazaoSocialReceita,
                     CPF = c.CPF,
                     TipoPessoa = c.TipoPessoa,
                     TipoSancao = c.TipoSancao,
                 })
                .ToListAsync();

        }

    }
}
