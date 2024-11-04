using CGEODP.Core.DomainObjects;

namespace CGEODP.Core.Data
{
    public interface IRepositoryRead<T> : IDisposable where T : class, IAggregateRoot
    {
        Task<PagedResult<T>> Listar(int pageNumber, int pageSize);
        Task<T> ObterId(Guid id);

    }
}
