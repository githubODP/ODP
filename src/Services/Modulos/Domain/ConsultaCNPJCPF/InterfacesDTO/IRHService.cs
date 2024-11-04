using Domain.ConsultaCNPJCPF.DTO.RecursosHumanos;


namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface IRHService
    {

        Task<BuscaRHDTO> BuscaCPF(string cpf);
    }
}
