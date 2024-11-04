using ODP.Web.UI.Models.Consultas.DTOViewModels.GovernoFederal;
using System.Threading.Tasks;

namespace ODP.Web.UI.Models.Consultas.InterfaceDTO
{
    public interface IGovernoFederalServicos
    {
        Task<BuscaFederalDTO> BuscarCNPJ(string cnpj);
        Task<BuscaFederalDTO> BuscarCPF(string cpf);
    }
}
