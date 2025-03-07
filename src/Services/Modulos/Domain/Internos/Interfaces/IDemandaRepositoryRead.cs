using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Domain.Internos.Entidade;

namespace Domain.Internos.Interfaces
{
    public interface IDemandaRepositoryRead : IRepositoryRead<DemandasInternas>, IBuscaInfo<DemandasInternas>
    {
        Task<PagedResult<DemandasInternas>> Listar(
           int pageNumber,
           int pageSize,
           string termo = null);
    }
}
