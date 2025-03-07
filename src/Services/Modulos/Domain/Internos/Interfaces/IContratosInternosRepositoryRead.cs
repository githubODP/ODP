using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Domain.Internos.Entidade;

namespace Domain.Internos.Interfaces
{
    public interface IContratosInternosRepositoryRead : IRepositoryRead<ContratosInternos>
    {
        Task<PagedResult<ContratosInternos>> Listar(
            int pageNumber,
            int pageSize,
            string termo = null);
    }
}
