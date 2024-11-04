using ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TCE;
using System.Threading.Tasks;


namespace ODP.Web.UI.Models.Consultas.InterfaceDTO
{
    public interface ITCEServicos
    {
        Task<BuscaTCEDTO> BuscarCNPJ(string cnpj);
        Task<BuscaTCEDTO> BuscarCPF(string cpf);
    }
}
