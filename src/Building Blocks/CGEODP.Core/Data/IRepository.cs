using CGEODP.Core.DomainObjects;

namespace CGEODP.Core.Data
{
    public interface IRepository<T> : IDisposable where T : class, IAggregateRoot
    {
        Task Adicionar(T entity);
        Task Atualizar(T entity);
        Task Deletar(T entity);
    }
}
