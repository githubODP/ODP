using CGEODP.Core.DomainObjects;
using Domain.RecursosHumanos.Entidades;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;


namespace Infra.RecursosHumanos.RepositoriesRead
{
    public class DependenteRepositoryRead : RepositoryRead<Dependente>, IDependenteRepositoryRead, IAggregateRoot
    {
        public DependenteRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<Dependente> BuscarPorCPF(string cpf)
        {
            return await _context.Set<Dependente>()
                .AsNoTracking()
                .Where(c => c.CPFDependente == cpf)
                .Select(c => new Dependente
                {
                    NomeDependente = c.NomeDependente,
                    CPFDependente = c.CPFDependente,
                    TipoDependente = c.TipoDependente,
                    InicioDependente = c.InicioDependente,
                    FimDependente = c.FimDependente,
                    Funcionario = new Funcionario // Aqui você cria o objeto Funcionario
                    {
                        Nome = c.Funcionario.Nome,
                        CPF = c.Funcionario.CPF,
                        Orgao = c.Funcionario.Orgao
                    }
                })
                .FirstOrDefaultAsync(); // Retorna o primeiro ou null
        }
    }
}

