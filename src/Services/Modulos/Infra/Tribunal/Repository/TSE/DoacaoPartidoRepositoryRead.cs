
using Domain.Tribunal.Entidades.TSE;
using Domain.Tribunal.Interfaces.TSE;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.Tribunal.Repository.TSE
{
    public class DoacaoPartidoRepositoryRead : RepositoryRead<DoacaoPartido>, IDoacaoPartidoRepositoryRead
    {
        public DoacaoPartidoRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<DoacaoPartido>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<DoacaoPartido>> BuscarCNPJ(string cnpj)
        //{
        //    // Decodifica o CNPJ, caso esteja URL-encoded
        //    cnpj = HttpUtility.UrlDecode(cnpj);

        //    return await _context.Set<DoacaoPartido>()
        //             .AsNoTracking()
        //             .Where(c => c.CNPJCPFDoador == cnpj)
        //             .ToListAsync();
        //}

        public async Task<List<DoacaoPartido>> BuscarCPF(string cpf)
        {
            return await _context.Set<DoacaoPartido>()
                     .AsNoTracking()
                     .Where(c => c.CNPJCPFDoador == cpf)
                     .Select(c => new DoacaoPartido
                     {
                         AnoEleicao = c.AnoEleicao,
                         Municipio = c.Municipio,
                         UF = c.UF,
                         NomePartido = c.NomePartido,
                         NomeDoador = c.NomeDoador,
                         CNPJCPFDoador = c.CNPJCPFDoador,
                         DescricaoReceita = c.DescricaoReceita,
                         DescricaoEspecieReceita = c.DescricaoEspecieReceita,
                         ValorReceita = c.ValorReceita,
                     })
                     .ToListAsync();
        }




    }
}
