

using Domain.ConsultaCNPJCPF.DTO.Dividas;

namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface IDividasService
    {
        Task<BuscaDividasDTO> BuscarCNPJ(string cnpj);
        Task<BuscaDividasDTO> BuscarCPF(string cpf);
    }
}
