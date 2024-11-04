using ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TSE;
using System.Threading.Tasks;

namespace ODP.Web.UI.Models.Consultas.InterfaceDTO
{
    public interface ITSEServicos
    {
        Task<BuscaTSEDTO> BuscarCNPJ(string cnpj);
        Task<BuscaTSEDTO> BuscarCPF(string cpf);
    }
}
