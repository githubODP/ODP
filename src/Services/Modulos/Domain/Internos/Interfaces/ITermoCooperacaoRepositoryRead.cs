using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Domain.Corregedoria.Entidade;
using Domain.Internos.Entidade;

namespace Domain.Internos.Interfaces
{
    public interface ITermoCooperacaoRepositoryRead : IRepositoryRead<TermoCooperacao>
    {
        Task<PagedResult<TermoCooperacao>> ListarComFiltrosAsync(int pageNumber, int pageSize);

        Task<TermoCooperacao> ObterID(Guid id);
    }
}
