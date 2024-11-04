
using ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TCU;
using System.Threading.Tasks;

namespace ODP.Web.UI.Models.Consultas.InterfaceDTO
{
    public interface ITCUServicos
    {
        Task<BuscaTCUDTO> BuscarCNPJ(string cnpj);
        Task<BuscaTCUDTO> BuscarCPF(string cpf);
    }
}
