

using Domain.RecursosHumanos.Entidades;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.RecursosHumanos.RepositoriesRead
{
    public class FuncionarioRepositoryRead : RepositoryRead<Funcionario>, IFuncionarioRepositoryRead
    {
        public FuncionarioRepositoryRead(ObservatorioContext context) : base(context)
        {
        }


        public async Task<Funcionario> BuscarPorCPF(string cpf)
        {
            return await _context.Set<Funcionario>()
                .AsNoTracking()
                .Include(c => c.Dependentes) // Inclui os dependentes
                .Where(c => c.CPF == cpf)
                .Select(c => new Funcionario
                {
                    Ordinal = c.Ordinal,
                    Nome = c.Nome,
                    CPF = c.CPF,
                    Orgao = c.Orgao,
                    Simbolo = c.Simbolo,
                    Quadro = c.Quadro,
                    Lotacao = c.Lotacao,
                    Inicio = c.Inicio,
                    Desligamento = c.Desligamento,
                    Dependentes = c.Dependentes.Select(d => new Dependente
                    {
                        NomeDependente = d.NomeDependente,
                        CPFDependente = d.CPFDependente,
                        TipoDependente = d.TipoDependente
                    }).ToList()
                })
            .FirstOrDefaultAsync();



        }



    }
}

