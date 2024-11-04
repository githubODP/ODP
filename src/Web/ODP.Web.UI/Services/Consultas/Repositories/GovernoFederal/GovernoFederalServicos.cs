

using ODP.Web.UI.Models.Consultas.DTOViewModels.GovernoFederal;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas.Repositories.GovernoFederal
{
    public class GovernoFederalServicos : IGovernoFederalServicos
    {
        private readonly IAcordoLenienciaService _acordoLenienciaService;
        private readonly IAeronaveService _aeronaveService;
        private readonly IBeneficioContinuoService _beneficioService;
        private readonly ICeisService _ceisService;
        private readonly IExpulsoFederalService _expulsoFederalService;
        private readonly IPEPService _pepService;
        private readonly ISeguroDefesoService _seguroDefesoService;
        private readonly ITrabalhoEscravoService _trabalhoEscravoService;


        public GovernoFederalServicos(IAcordoLenienciaService acordoLenienciaService,
                                      IAeronaveService aeronaveService,
                                      IBeneficioContinuoService beneficioService,
                                      ICeisService ceisService,
                                      IExpulsoFederalService expulsoFederalService,
                                      IPEPService pepService,
                                      ISeguroDefesoService seguroDefesoService,
                                      ITrabalhoEscravoService trabalhoEscravoService)
        {
            _acordoLenienciaService = acordoLenienciaService;
            _aeronaveService = aeronaveService;
            _beneficioService = beneficioService;
            _ceisService = ceisService;
            _expulsoFederalService = expulsoFederalService;
            _pepService = pepService;
            _seguroDefesoService = seguroDefesoService;
            _trabalhoEscravoService = trabalhoEscravoService;
        }

        public async Task<BuscaFederalDTO> BuscarCNPJ(string cnpj)
        {
            var acordo = await _acordoLenienciaService.BuscarCNPJ(cnpj);
            var aeronave = await _aeronaveService.BuscarCNPJ(cnpj);
            var ceis = await _ceisService.BuscarCNPJ(cnpj);
            var trabalho = await _trabalhoEscravoService.BuscarCNPJ(cnpj);

            return new BuscaFederalDTO
            {
                AcordosLeniencia = acordo,
                Aeronave = aeronave,
                Ceis = ceis,
                TrabalhoEscravo = trabalho,
            };
        }

        public async Task<BuscaFederalDTO> BuscarCPF(string cpf)
        {

            var aeronave = await _aeronaveService.BuscarCPF(cpf);
            var beneficio = await _beneficioService.BuscarCPF(cpf);
            var ceis = await _ceisService.BuscarCPF(cpf);
            var expulso = await _expulsoFederalService.BuscarCPF(cpf);
            var pep = await _pepService.BuscarCPF(cpf);
            var seguro = await _seguroDefesoService.BuscarCPF(cpf);
            var trabalho = await _trabalhoEscravoService.BuscarCPF(cpf);

            return new BuscaFederalDTO
            {

                Aeronave = aeronave,
                BeneficioContinuo = beneficio,
                Ceis = ceis,
                ExpulsoFederal = expulso,
                PEP = pep,
                SeguroDefeso = seguro,
                TrabalhoEscravo = trabalho,
            };
        }

    }
}
