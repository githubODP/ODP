using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Domain.Corregedoria.Enum;
using Domain.Internos.Entidade;

namespace Domain.Internos.Interfaces
{
    public interface ITermoCooperacaoRepositoryRead : IRepositoryRead<TermoCooperacao>
    {



        Task<List<TermoCooperacao>> ListarEnvio();
        Task<List<string>> EnviarAlertasPorEmail();
        Task<PagedResult<TermoCooperacao>> Listar(
            int pageNumber,
            int pageSize,
            string termo = null);
    }
}
