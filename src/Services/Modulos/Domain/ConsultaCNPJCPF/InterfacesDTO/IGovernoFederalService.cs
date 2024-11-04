using Domain.ConsultaCNPJCPF.DTO.GovernoFederal;

namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface IGovernoFederalService
    {
        Task<BuscaFederalDTO> BuscarCNPJ(string cnpj);
        Task<BuscaFederalDTO> BuscarCPF(string cpf);
    }
}
