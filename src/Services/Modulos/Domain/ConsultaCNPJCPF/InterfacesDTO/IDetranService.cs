

using Domain.ConsultaCNPJCPF.DTO.Detran;

namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface IDetranService
    {
        Task<BuscaDetranDTO> BuscarCNPJ(string cnpj);
        Task<BuscaDetranDTO> BuscarCPF(string cpf);
    }
}
