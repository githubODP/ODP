using Domain.Tribunal.Entidades.TSE;
using Domain.Tribunal.Interfaces.TSE;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.Tribunal.Repository.TSE
{
    public class DoacaoGeralRepositoryRead : RepositoryRead<DoacaoGeral>, IDoacaoGeralRepositoryRead
    {
        public DoacaoGeralRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<DoacaoGeral>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DoacaoGeral>> BuscarCPF(string cpf)
        {
            return await _context.Set<DoacaoGeral>()
                                  .AsNoTracking()
                                  .Where(c => c.CPF == cpf)
            .Select(c => new DoacaoGeral
            {
                AnoEleicao = c.AnoEleicao,
                UF = c.UF,
                Nome = c.Nome,
                CPF = c.CPF,
                NomePartido = c.NomePartido,
                SiglaPartido = c.SiglaPartido,
                DescricaoReceita = c.DescricaoReceita,
                ValorDoacao = c.ValorDoacao,

            })
            .ToListAsync();
        }


    }
}
