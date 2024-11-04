using CGEODP.Core.Data;
using Domain.Detran.Entidades;

namespace Domain.Detran.Interfaces
{
    public interface IVeiculoIndisponivelRepositoryRead : IRepositoryRead<VeiculoIndispAdmin>
    {
        Task<VeiculoIndispAdmin> BuscarPorCNPJ(string cnpj);
        Task<VeiculoIndispAdmin> BuscarPorCPF(string cpf);
    }
}
