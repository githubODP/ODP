using Domain.Tribunal.Entidades.TCU;
using Domain.Tribunal.Interfaces.TCU;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.Tribunal.Repository.TCU
{
    public class ContaEleitoralIrregularRepositoryRead : RepositoryRead<ContaEleitoralIrregular>, IContaEleitoralIrregularRepositoryRead
    {
        public ContaEleitoralIrregularRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<ContaEleitoralIrregular>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContaEleitoralIrregular>> BuscarCPF(string cpf)
        {
            return await _context.Set<ContaEleitoralIrregular>()
                                .AsNoTracking()
                                .Where(c => c.CPF == cpf)
            .Select(c => new ContaEleitoralIrregular
            {
                Nome = c.Nome,
                CPF = c.CPF,
                CargoFuncao = c.CargoFuncao,
                Municipio = c.Municipio,
                UF = c.UF,
                Deliberacao = c.Deliberacao,
                DataJulgado = c.DataJulgado,
                DataFinal = c.DataFinal,
                Processo = c.Processo,
            })
            .ToListAsync();
        }

    }
}