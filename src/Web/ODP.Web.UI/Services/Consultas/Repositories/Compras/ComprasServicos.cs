using ODP.Web.UI.Models.Consultas.DTOViewModels.Compras;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Interfaces.Compras;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas.Repositories.Compras
{
    public class ComprasServicos : IComprasServicos
    {
        private readonly IContratoService _contratoService;
        private readonly IDispensaService _dispensaService;
        private readonly ILicitacaoService _licitacaoService;

        public ComprasServicos(
             IContratoService contratoService,
             IDispensaService dispensaService,
             ILicitacaoService licitacaoService)
        {
            _contratoService = contratoService;
            _dispensaService = dispensaService;
            _licitacaoService = licitacaoService;
        }

        public async Task<BuscaComprasDTO> BuscarCNPJ(string cnpj)
        {
            var contrato = await _contratoService.BuscarCNPJ(cnpj);
            var dispensa = await _dispensaService.BuscarCNPJ(cnpj);
            var licitacao = await _licitacaoService.BuscarCNPJ(cnpj);


            return new BuscaComprasDTO
            {
                Contrato = contrato,
                Dispensa = dispensa,
                Licitacao = licitacao,


            };
        }


        public async Task<BuscaComprasDTO> BuscarCPF(string cpf)
        {
            var contrato = await _contratoService.BuscarCPF(cpf);



            return new BuscaComprasDTO
            {

                Contrato = contrato,

            };
        }


    }
}
