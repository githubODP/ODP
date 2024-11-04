using Domain.ConsultaCNPJCPF.DTO.GovernoEstadual;

namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface IGovernoEstadualService
    {
        Task<BuscaEstadualDTO> BuscarCNPJ(string cnpj);
        Task<BuscaEstadualDTO> BuscarCPF(string cpf);
    }
}
