using CGEODP.Core.Data;
using Domain.Corregedoria.Entidade;


namespace Domain.Corregedoria.Interfaces
{
    public interface IInstauracaoRepositoryRead : IRepositoryRead<Instauracao>
    {
        Task<Instauracao> BuscarPorCNPJ(string cnpj);
        Task<Instauracao> BuscarPorCPF(string cpf);
    }
}
