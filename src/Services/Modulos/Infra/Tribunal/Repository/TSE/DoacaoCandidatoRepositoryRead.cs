using Domain.Tribunal.Entidades.TSE;
using Domain.Tribunal.Interfaces.TSE;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Tribunal.Repository.TSE
{
    public class DoacaoCandidatoRepositoryRead : RepositoryRead<DoacaoCandidato>, IDoacaoCandidatoRepositoryRead
    {
        public DoacaoCandidatoRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<DoacaoCandidato>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<DoacaoCandidato>> BuscarCNPJ(string cnpj)
        //{
        //    // Decodifica o CNPJ, caso esteja URL-encoded
        //    cnpj = HttpUtility.UrlDecode(cnpj);
        //    return await _context.Set<DoacaoCandidato>()
        //                          .AsNoTracking()
        //                          .Where(c => c.CNPJPrestadorConta == cnpj)
        //    .Select(c => new DoacaoCandidato
        //     {
        //         NomeCandidato = c.NomeCandidato,
        //         NomeDoador = c.NomeDoador,
        //         NomePartido = c.NomePartido,
        //         CNPJPrestadorConta = c.CNPJPrestadorConta,
        //         SiglaPartido = c.SiglaPartido,
        //         ValorDoacao = c.ValorDoacao,
        //         UF = c.UF,
        //         CPFDoador = c.CPFDoador,
        //         AnoEleicao = c.AnoEleicao,
        //         Cargo = c.Cargo,
        //     })
        //    .ToListAsync();
        //}

        public async Task<List<DoacaoCandidato>> BuscarCPF(string cpf)
        {
            return await _context.Set<DoacaoCandidato>()
                                  .AsNoTracking()
                                  .Where(c => c.CPFDoador == cpf)
            .Select(c => new DoacaoCandidato
             {
                AnoEleicao = c.AnoEleicao,
                Municipio = c.Municipio,
                UF = c.UF,
                NomeCandidato = c.NomeCandidato,    
                CPFCandidato = c.CPFCandidato,
                Cargo = c.Cargo,
                NomePartido = c.NomePartido,
                NomeDoador = c.NomeDoador,
                CPFDoador = c.CPFDoador,
                DescricaoReceita = c.DescricaoReceita,  
                ValorDoacao = c.ValorDoacao,
             })
            .ToListAsync();
        }
    }
}
