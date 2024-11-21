using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.GovernoFederal.Repositories
{
    public class ExpulsoFederalRepositoryRead : RepositoryRead<ExpulsoFederal>, IExpulsoFederalRepositoryRead
    {
        public ExpulsoFederalRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<ExpulsoFederal>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }



        public async Task<List<ExpulsoFederal>> BuscarCPF(string cpf)
        {

            return await _context.Set<ExpulsoFederal>()
                .AsNoTracking()
                .Where(c => c.CPF == cpf)
                .Select(c => new ExpulsoFederal
                {
                    Nome = c.Nome,
                    CPF = c.CPF,
                    Punicao = c.Punicao,
                    NumeroProcesso = c.NumeroProcesso,
                    DataPublicacao = c.DataPublicacao,
                })
                .ToListAsync();

        }


    }
}
