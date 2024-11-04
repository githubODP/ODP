using ODP.Web.UI.Models.Consultas.DTOViewModels.GovernoEstadual;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Interfaces.GovernoEstadual;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas.Repositories.GovernoEstadual
{
    public class GovernoEstadualServicos : IGovernoEstadualServicos
    {
        private readonly IAmbientalService _ambientalService;
        private readonly IPADVService _padvService;

        public GovernoEstadualServicos(IAmbientalService ambientalService,
                                      IPADVService padvService)
        {
            _ambientalService = ambientalService;
            _padvService = padvService;
        }

        public async Task<BuscaEstadualDTO> BuscarCNPJ(string cnpj)
        {
            var ambiental = await _ambientalService.BuscarCNPJ(cnpj);
            var padv = await _padvService.BuscarCNPJ(cnpj);

            return new BuscaEstadualDTO
            {
                Ambiental = ambiental,
                PADV = padv,
            };
        }

        public async Task<BuscaEstadualDTO> BuscarCPF(string cpf)
        {
            var ambiental = await _ambientalService.BuscarCPF(cpf);


            return new BuscaEstadualDTO
            {
                Ambiental = ambiental,

            };
        }
    }
}
