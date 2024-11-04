using ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TCE;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCE;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas.Repositories.Tribunal.TCE
{
    public class TCEServicos : ITCEServicos
    {
        private readonly ICNPJRestritoService _cnpjRestritoService;
        private readonly ICPFRestritoService _cpfRestritoService;
        private readonly IInadimplenteService _inadimplenteService;
        private readonly IIrregularidadeService _irregularidadeService;

        public TCEServicos(ICNPJRestritoService cnpjRestritoService,
                          ICPFRestritoService cpfRestritoService,
                          IInadimplenteService inadimplenteService,
                          IIrregularidadeService irregularidadeService)
        {
            _cnpjRestritoService = cnpjRestritoService;
            _cpfRestritoService = cpfRestritoService;
            _inadimplenteService = inadimplenteService;
            _irregularidadeService = irregularidadeService;
        }

        public async Task<BuscaTCEDTO> BuscarCNPJ(string cnpj)
        {
            var cnpjrestrito = await _cnpjRestritoService.BuscarCNPJ(cnpj);
            var inadimplente = await _inadimplenteService.BuscarCNPJ(cnpj);


            return new BuscaTCEDTO
            {
                CNPJRestrito = cnpjrestrito,
                Inadimplente = inadimplente,

            };
        }

        public async Task<BuscaTCEDTO> BuscarCPF(string cpf)
        {

            var cpfrestrito = await _cpfRestritoService.BuscarCPF(cpf);
            var inadimplente = await _inadimplenteService.BuscarCPF(cpf);
            var irregular = await _irregularidadeService.BuscarCPF(cpf);

            return new BuscaTCEDTO
            {

                CPFRestrito = cpfrestrito,
                Inadimplente = inadimplente,
                Irregularidade = irregular,
            };
        }
    }
}
