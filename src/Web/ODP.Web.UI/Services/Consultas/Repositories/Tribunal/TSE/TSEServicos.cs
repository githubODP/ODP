using ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TSE;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Interfaces.Tribunal.TSE;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas.Repositories.Tribunal.TSE
{
    public class TSEServicos : ITSEServicos
    {
        private readonly IDoacaoCandidatoService _doacaoCandidatoService;
        private readonly IDoacaoGeralService _doacaoGeralService;
        private readonly IDoacaoPartidoGeralService _doacaoPartidoGeralService;
        private readonly IFornecedorPartidoService _fornecedorPartidoService;
        private readonly IFornecedorCandidatoService _fornecedorCandidatoService;
        private readonly IDoacaoPartidoService _doacaoPartidoService;


        public TSEServicos(IDoacaoCandidatoService doacaoCandidatoService,
                           IDoacaoGeralService doacaoGeralService,
                           IDoacaoPartidoGeralService doacaoPartidoGeralService,
                           IFornecedorPartidoService fornecedorPartidoService,
                           IFornecedorCandidatoService fornecedorCandidatoService,
                           IDoacaoPartidoService doacaoPartidoService)
        {
            _doacaoCandidatoService = doacaoCandidatoService;
            _doacaoGeralService = doacaoGeralService;
            _doacaoPartidoGeralService = doacaoPartidoGeralService;
            _fornecedorPartidoService = fornecedorPartidoService;
            _fornecedorCandidatoService = fornecedorCandidatoService;
            _doacaoPartidoService = doacaoPartidoService;
        }

        public async Task<BuscaTSEDTO> BuscarCNPJ(string cnpj)
        {
            var doacaocandidato = await _doacaoCandidatoService.BuscarCNPJ(cnpj);
            var doacaoPartidoGeral = await _doacaoPartidoGeralService.BuscarCNPJ(cnpj);
            var fornecedorPartido = await _fornecedorPartidoService.BuscarCNPJ(cnpj);
            var fornecedorCandidato = await _fornecedorCandidatoService.BuscarCNPJ(cnpj);
            var doacaoPartido = await _doacaoPartidoService.BuscarCNPJ(cnpj);

            return new BuscaTSEDTO
            {
                DoacaoCandidato = doacaocandidato,
                DoacaoPartidoGeral = doacaoPartidoGeral,
                FornecedorPartido = fornecedorPartido,
                FornecedorCandidato = fornecedorCandidato,
                DoacaoPartido = doacaoPartido,
            };
        }



        public async Task<BuscaTSEDTO> BuscarCPF(string cpf)
        {
            var doacaocandidato = await _doacaoCandidatoService.BuscarCPF(cpf);
            var doacaoGeral = await _doacaoGeralService.BuscarCPF(cpf);
            var doacaoPartidoGeral = await _doacaoPartidoGeralService.BuscarCPF(cpf);
            var fornecedorPartido = await _fornecedorPartidoService.BuscarCPF(cpf);
            var fornecedorCandidato = await _fornecedorCandidatoService.BuscarCPF(cpf);
            var doacaoPartido = await _doacaoPartidoService.BuscarCPF(cpf);

            return new BuscaTSEDTO
            {
                DoacaoCandidato = doacaocandidato,
                DoacaoGeral = doacaoGeral,
                DoacaoPartidoGeral = doacaoPartidoGeral,
                FornecedorPartido = fornecedorPartido,
                FornecedorCandidato = fornecedorCandidato,
                DoacaoPartido = doacaoPartido,

            };

        }
    }
}
