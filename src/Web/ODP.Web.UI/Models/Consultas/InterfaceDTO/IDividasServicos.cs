

using ODP.Web.UI.Models.Consultas.DTOViewModels.Dividas;
using System.Threading.Tasks;

namespace ODP.Web.UI.Models.Consultas.InterfaceDTO
{
    public interface IDividasServicos
    {
        Task<BuscaDividasDTO> BuscarCNPJ(string cnpj);
        Task<BuscaDividasDTO> BuscarCPF(string cpf);
    }
}
