using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Domain.DueDiligence.Entidade;

namespace Domain.DueDiligence.Interfaces
{
    public interface IDueRepositoryRead : IRepositoryRead<Comissionado>
    {

        Task<List<Comissionado>> BuscarPorCPF(string cpf);
        Task<PagedResult<Comissionado>> ListarCOmFiltroAsync(
            int pageNumber,
            int pageSize,
            string nome = null,
            string CPF = null,
            string protocolo = null);
    }
}
