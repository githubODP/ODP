using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Domain.DueDiligence.Entidade;

namespace Domain.DueDiligence.Interfaces
{
    public interface IAnaliseRepositoryRead : IRepositoryRead<Analise>
    {
        Task<PagedResult<AnaliseResponseDTO>> ListarDadosAdicionais(int pageNumber, int pageSize);
    }
}
