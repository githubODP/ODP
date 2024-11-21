using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.GovernoFederal.Repositories
{
    public class PEPRepositoryRead : RepositoryRead<PEP>, IPEPRepositoryRead
    {
        public PEPRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<PEP>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }



        public async Task<List<PEP>> BuscarCPF(string cpf)
        {

            return await _context.Set<PEP>()
                .AsNoTracking()
                .Where(c => c.CPF == cpf)
                .Select(c => new PEP
                {
                    Nome = c.Nome,
                    CPF = c.CPF,
                    Funcao = c.Funcao,
                    Orgao = c.Orgao,
                    DataInicio = c.DataInicio,
                    DataFim = c.DataFim,
                    DataFimCarencia = c.DataFimCarencia,
                })
                .ToListAsync();

        }
    }
}
