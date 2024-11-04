

using Domain.ConsultaCNPJCPF.DTO.Tribunais.TCU;

namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface ITCUService
    {
        Task<BuscaTCUDTO> BuscaCNPJ(string cnpj);
        Task<BuscaTCUDTO> BuscarCPF(string cpf);
    }
}
