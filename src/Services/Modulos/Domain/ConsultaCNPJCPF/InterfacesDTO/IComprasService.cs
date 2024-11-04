

using Domain.ConsultaCNPJCPF.DTO.Compras;

namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface IComprasService
    {
        Task<BuscaComprasDTO> BuscarCNPJ(string cnpj);
        Task<BuscaComprasDTO> BuscarCPF(string cpf);

    }
}
