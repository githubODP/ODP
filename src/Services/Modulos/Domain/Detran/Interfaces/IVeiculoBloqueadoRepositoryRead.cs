
using CGEODP.Core.Data;
using Domain.Detran.Entidades;

namespace Domain.Detran.Interfaces
{
    public interface IVeiculoBloqueadoRepositoryRead : IRepositoryRead<VeiculoBloqueioRoubo>
    {
        Task<VeiculoBloqueioRoubo> BuscarPorCNPJ(string cnpj);
        Task<VeiculoBloqueioRoubo> BuscarPorCPF(string cpf);
    }
}
