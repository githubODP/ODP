using ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TCU;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCU;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas.Repositories.Tribunal.TCU
{
    public class TCUServicos : ITCUServicos
    {
        private readonly IContaEleitoralIrregularService _contaeleitoralService;
        private readonly IContaIrregularService _contairregularService;
        private readonly IInabilitadoService _inabilitadoService;
        private readonly IInidoneoService _inidoneoService;

        public TCUServicos(IContaEleitoralIrregularService contaeleitoralService,
                          IContaIrregularService contairregularService,
                          IInabilitadoService inabilitadoService,
                          IInidoneoService inidoneoService)
        {
            _contaeleitoralService = contaeleitoralService;
            _contairregularService = contairregularService;
            _inabilitadoService = inabilitadoService;
            _inidoneoService = inidoneoService;
        }

        public async Task<BuscaTCUDTO> BuscarCNPJ(string cnpj)
        {

            var contairregular = await _contairregularService.BuscarCNPJ(cnpj);
            var inidoneo = await _inidoneoService.BuscarCNPJ(cnpj);

            return new BuscaTCUDTO
            {

                ContaIrregular = contairregular,
                Inidoneo = inidoneo
            };
        }

        public async Task<BuscaTCUDTO> BuscarCPF(string cpf)
        {
            var contaeleitoral = await _contaeleitoralService.BuscarCPF(cpf);
            var contairregular = await _contairregularService.BuscarCPF(cpf);
            var inabilitado = await _inabilitadoService.BuscarCPF(cpf);


            return new BuscaTCUDTO
            {
                ContaEleitoralIrregular = contaeleitoral,
                ContaIrregular = contairregular,
                Inabilitado = inabilitado,

            };
        }
    }
}
