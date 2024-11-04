
using CGEODP.Core.Data;
using Domain.Detran.Entidades;

namespace Domain.Detran.Interfaces
{
    public interface IVeiculoOrdemJudicialRepositoryRead : IRepositoryRead<VeiculoOrdemJudicial>
    {
        Task<VeiculoOrdemJudicial> BuscarPorCNPJ(string cnpj);
        Task<VeiculoOrdemJudicial> BuscarPorCPF(string cpf);
    }
}
