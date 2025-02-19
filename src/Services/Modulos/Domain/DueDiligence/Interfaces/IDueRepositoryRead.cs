using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Domain.DueDiligence.Entidade;

namespace Domain.DueDiligence.Interfaces
{
    public interface IDueRepositoryRead : IRepositoryRead<Comissionado>
    {

        Task<List<Comissionado>> ObterPorProtocolo(string nroProtocolo);

        Task<List<Comissionado>> BuscarPorCPF(string cpf);
        Task<PagedResult<Comissionado>> Listar(
            int pageNumber,
            int pageSize,
            string termo = null);
    }
}
