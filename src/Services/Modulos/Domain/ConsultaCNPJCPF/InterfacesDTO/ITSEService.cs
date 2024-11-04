using Domain.ConsultaCNPJCPF.DTO.Tribunais.TSE;

namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface ITSEService
    {
        Task<BuscaTSEDTO> BuscaCNPJ(string cnpj);
        Task<BuscaTSEDTO> BuscarCPF(string cpf);
    }
}
