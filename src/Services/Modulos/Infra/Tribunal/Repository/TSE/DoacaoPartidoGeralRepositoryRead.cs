using Domain.Tribunal.Entidades.TSE;
using Domain.Tribunal.Interfaces.TSE;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.Tribunal.Repository.TSE
{
    public class DoacaoPartidoGeralRepositoryRead : RepositoryRead<DoacaoPartidoGeral>, IDoacaoPartidoGeralRepositoryRead
    {
        public DoacaoPartidoGeralRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<DoacaoPartidoGeral>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DoacaoPartidoGeral>> BuscarCPF(string cpf)
        {
            return await _context.Set<DoacaoPartidoGeral>()
                                .AsNoTracking()
                                .Where(c => c.CPF == cpf)
            .Select(c => new DoacaoPartidoGeral
            {
                AnoEleicao = c.AnoEleicao, 
                UF = c.UF,
                Nome = c.Nome,
                CPF = c.CPF,
                NomePartido = c.NomePartido,
                SiglaPartido = c.SiglaPartido,
                DescricaoReceita = c.DescricaoReceita,
                ValorReceita = c.ValorReceita,
            })
            .ToListAsync();
        }
    }
}
