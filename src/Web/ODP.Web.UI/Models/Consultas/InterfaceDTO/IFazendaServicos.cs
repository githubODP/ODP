using ODP.Web.UI.Models.Consultas.DTOViewModels.Fazenda;
using System.Threading.Tasks;

namespace ODP.Web.UI.Models.Consultas.InterfaceDTO
{
    public interface IFazendaServicos
    {
        Task<BuscaFazendaDTO> BuscarCNPJ(string cnpj);
        Task<BuscaFazendaDTO> BuscarCPF(string cpf);
    }
}
