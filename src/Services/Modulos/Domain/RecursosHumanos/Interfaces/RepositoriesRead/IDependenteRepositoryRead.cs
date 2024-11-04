using CGEODP.Core.Data;
using Domain.RecursosHumanos.Entidades;



namespace Domain.RecursosHumanos.Interfaces.RepositoriesRead
{
    public interface IDependenteRepositoryRead : IRepositoryRead<Dependente>
    {
        Task<Dependente> BuscarPorCPF(string cpf);

    }
}
