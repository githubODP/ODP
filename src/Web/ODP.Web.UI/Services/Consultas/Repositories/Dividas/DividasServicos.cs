

using ODP.Web.UI.Models.Consultas.DTOViewModels.Dividas;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Interfaces.Dividas;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas.Repositories.Dividas
{
    public class DividasServicos : IDividasServicos
    {
        private readonly IDividaFGTsService _dividaFGTsService;
        private readonly IDividaPrevidenciaService _dividaPrevidenciaService;
        private readonly IDividaNaoPrevidenciaService _dividanaoPrevidenciaService;

        public DividasServicos(IDividaFGTsService dividaFGTsService,
                            IDividaPrevidenciaService dividaPrevidenciaService,
                            IDividaNaoPrevidenciaService dividanaoPrevidenciaService)
        {
            _dividaFGTsService = dividaFGTsService;
            _dividaPrevidenciaService = dividaPrevidenciaService;
            _dividanaoPrevidenciaService = dividanaoPrevidenciaService;
        }

        public async Task<BuscaDividasDTO> BuscarCNPJ(string cnpj)
        {
            var dividafgts = await _dividaFGTsService.BuscarCNPJ(cnpj);
            var dividaprevidencia = await _dividaPrevidenciaService.BuscarCNPJ(cnpj);
            var dividanaoprevidencia = await _dividanaoPrevidenciaService.BuscarCNPJ(cnpj);

            return new BuscaDividasDTO
            {
                DividaFGTS = dividafgts,
                DividaPrevidenciaria = dividaprevidencia,
                DividaNaoPrevidenciaria = dividanaoprevidencia,
            };
        }


        public async Task<BuscaDividasDTO> BuscarCPF(string cpf)
        {
            var dividafgts = await _dividaFGTsService.BuscarCPF(cpf);
            var dividaprevidencia = await _dividaPrevidenciaService.BuscarCPF(cpf);
            var dividanaoprevidencia = await _dividanaoPrevidenciaService.BuscarCPF(cpf);


            return new BuscaDividasDTO
            {
                DividaFGTS = dividafgts,
                DividaPrevidenciaria = dividaprevidencia,
                DividaNaoPrevidenciaria = dividanaoprevidencia,
            };
        }
    }
}
