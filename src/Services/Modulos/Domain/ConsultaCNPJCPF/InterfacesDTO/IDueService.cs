

using Domain.ConsultaCNPJCPF.DTO.DueDiligence;

namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface IDueService
    {
        Task<BuscaDueDTO> BuscarCPF(string cpf);
    }
}
