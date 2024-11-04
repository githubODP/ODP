using Domain.ConsultaCNPJCPF.DTO.Tribunais.TCE;


namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface ITCEService
    {
        Task<BuscaTCEDTO> BuscaCNPJ(string cnpj);
        Task<BuscaTCEDTO> BuscarCPF(string cpf);
    }
}
