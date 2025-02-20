using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Domain.Internos.Entidade;

namespace Domain.Internos.Interfaces
{
    public interface ITermoCooperacaoRepositoryRead : IRepositoryRead<TermoCooperacao>
    {

        Task<TermoCooperacao> ObterProtocolo(string protocolo);

        Task<List<TermoCooperacao>> ListarEnvio();
        Task<List<string>> EnviarAlertasPorEmail();
        Task<PagedResult<TermoCooperacao>> ListarComFiltroAsync(
            int pageNumber,
            int pageSize,
            string protocolo = null);
    }
}
