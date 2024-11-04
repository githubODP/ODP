using ODP.Web.UI.Models.Consultas.DTOViewModels.Fazenda;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Interfaces.Fazenda;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas.Repositories.Fazenda
{
    public class FazendaServicos : IFazendaServicos
    {
        private readonly IJuceparService _juceparService;
        private readonly INFEletronicaFederalService _infeletronicaFederalService;
        private readonly INFEletronicaService _nfeletronicaService;

        public FazendaServicos(IJuceparService juceparService,
                              INFEletronicaFederalService infeletronicaFederalService,
                              INFEletronicaService nfeletronicaService)
        {
            _juceparService = juceparService;
            _infeletronicaFederalService = infeletronicaFederalService;
            _nfeletronicaService = nfeletronicaService;
        }

        public async Task<BuscaFazendaDTO> BuscarCNPJ(string cnpj)
        {
            var junta = await _juceparService.BuscarCNPJ(cnpj);
            var nfefederal = await _infeletronicaFederalService.BuscarCNPJ(cnpj);
            var nfeeletronica = await _nfeletronicaService.BuscarCNPJ(cnpj);

            return new BuscaFazendaDTO
            {
                Jucepar = junta,
                NFEletronicaFederal = nfefederal,
                NFEletronica = nfeeletronica,
            };
        }

        public async Task<BuscaFazendaDTO> BuscarCPF(string cpf)
        {
            var junta = await _juceparService.BuscarCPF(cpf);
            var nfefederal = await _infeletronicaFederalService.BuscarCPF(cpf);

            return new BuscaFazendaDTO
            {
                Jucepar = junta,
                NFEletronicaFederal = nfefederal,
            };
        }

    }
}
