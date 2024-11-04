

using ODP.Web.UI.Models.Consultas.DTOViewModels.Compras;
using System.Threading.Tasks;

namespace ODP.Web.UI.Models.Consultas.InterfaceDTO
{
    public interface IComprasServicos
    {
        Task<BuscaComprasDTO> BuscarCNPJ(string cnpj);
        Task<BuscaComprasDTO> BuscarCPF(string cpf);

    }
}
