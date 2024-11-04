using CGEODP.Core.Data;
using Domain.RecursosHumanos.Entidades;



namespace Domain.RecursosHumanos.Interfaces.RepositoriesRead
{
    public interface IFuncionarioRepositoryRead : IRepositoryRead<Funcionario>
    {
        Task<Funcionario> BuscarPorCPF(string cpf);

    }
}
