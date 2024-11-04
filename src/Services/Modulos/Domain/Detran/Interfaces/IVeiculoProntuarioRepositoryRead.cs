
using CGEODP.Core.Data;
using Domain.Detran.Entidades;

namespace Domain.Detran.Interfaces
{
    public interface IVeiculoProntuarioRepositoryRead : IRepositoryRead<VeiculoProntuario>
    {
        Task<VeiculoProntuario> BuscarPorCNPJ(string cnpj);
        Task<VeiculoProntuario> BuscarPorCPF(string cpf);
    }
}
