
using CGEODP.Core.Data;
using Domain.Detran.Entidades;

namespace Domain.Detran.Interfaces
{
    public interface IVeiculoCirculacaoRepositoryRead : IRepositoryRead<VeiculoCirculacao>
    {
        Task<VeiculoCirculacao> BuscarPorCNPJ(string cnpj);
        Task<VeiculoCirculacao> BuscarPorCPF(string cpf);
        Task<List<VeiculoCirculacao>> CNPJCPFAvancada(string cnpjcpf);
    }
}
