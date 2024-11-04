
using Domain.ConsultaCNPJCPF.DTO.Fazenda;

namespace Domain.ConsultaCNPJCPF.InterfacesDTO
{
    public interface IFazendaService
    {
        Task<BuscaFazendaDTO> BuscarCNPJ(string cnpj);
        Task<BuscaFazendaDTO> BuscarCPF(string cpf);
    }
}
