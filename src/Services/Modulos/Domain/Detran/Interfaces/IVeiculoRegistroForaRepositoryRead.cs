using CGEODP.Core.Data;
using Domain.Detran.Entidades;

namespace Domain.Detran.Interfaces
{
    public interface IVeiculoRegistroForaRepositoryRead : IRepositoryRead<VeiculoRegistroFora>
    {
        Task<VeiculoRegistroFora> BuscarPorCNPJ(string cnpj);
        Task<VeiculoRegistroFora> BuscarPorCPF(string cpf);
    }
}
