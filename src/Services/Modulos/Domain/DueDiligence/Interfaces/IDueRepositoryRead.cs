using CGEODP.Core.Data;
using Domain.DueDiligence.Entidade;

namespace Domain.DueDiligence.Interfaces
{
    public interface IDueRepositoryRead : IRepositoryRead<Comissionado>
    {

        Task<List<Comissionado>> BuscarPorCPF(string cpf);

    }
}
