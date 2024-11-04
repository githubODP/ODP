
using CGEODP.Core.Data;
using Domain.Detran.Entidades;


namespace Domain.Detran.Interfaces
{
    public interface IVeiculoBaixadoRepositoryRead : IRepositoryRead<VeiculoBaixado>
    {
        Task<VeiculoBaixado> BuscarPorCNPJ(string cnpj);
        Task<VeiculoBaixado> BuscarPorCPF(string cpf);
    }
}
