using CGEODP.Core.Data;
using CGEODP.Core.DomainObjects;
using Domain.Corregedoria.Entidade;


namespace Domain.Corregedoria.Interfaces
{
    public interface IInstauracaoRepositoryRead : IRepositoryRead<Instauracao>
    {
       

        Task<Instauracao> BuscarPorCNPJ(string cnpj);
        Task<Instauracao> BuscarPorCPF(string cpf);

        Task<PagedResult<Instauracao>> ListarComFiltrosAsync(
        int pageNumber,
        int pageSize,
        int? ano = null,
        string orgao = null,
        string procedimento = null,
        string decisao = null,
        string protocolo = null); // Protocolo adicionado como opcional
    }
}

