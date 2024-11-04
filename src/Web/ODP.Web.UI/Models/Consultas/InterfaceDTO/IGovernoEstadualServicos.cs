
using ODP.Web.UI.Models.Consultas.DTOViewModels.GovernoEstadual;
using System.Threading.Tasks;

namespace ODP.Web.UI.Models.Consultas.InterfaceDTO
{
    public interface IGovernoEstadualServicos
    {
        Task<BuscaEstadualDTO> BuscarCNPJ(string cnpj);
        Task<BuscaEstadualDTO> BuscarCPF(string cpf);
    }
}
