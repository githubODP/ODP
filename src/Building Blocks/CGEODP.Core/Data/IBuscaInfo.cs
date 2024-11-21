using CGEODP.Core.DomainObjects;

namespace CGEODP.Core.Data
{
    public interface IBuscaInfo<T> : IDisposable where T : class, IAggregateRoot
    {
        Task<List<T>> BuscarCNPJ(string cnpj);
        Task<List<T>> BuscarCPF(string cpf);
    }
}
