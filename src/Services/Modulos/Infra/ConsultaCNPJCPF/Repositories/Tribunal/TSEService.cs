using Domain.ConsultaCNPJCPF.DTO.Tribunais.TSE;
using Domain.ConsultaCNPJCPF.InterfacesDTO;
using Domain.Tribunal.Interfaces.TSE;

namespace Infra.ConsultaCNPJCPF.Repositories.Tribunal
{
    public class TSEService : ITSEService
    {
        private readonly IDoacaoCandidatoRepositoryRead _doocaocandidatorepositoryRead;
        private readonly IDoacaoGeralRepositoryRead _doacaoGeralRepositoryRead;
        private readonly IDoacaoPartidoGeralRepositoryRead _doacaoPartidoGeralRepositoryRead;
        private readonly IDoacaoPartidoRepositoryRead _doacaoPartidoRepositoryRead;
        private readonly IFornecedorCandidatoRepositoryRead _fornecedorCandidatoRepositoryRead;
        private readonly IFornecedorPartidoRepositoryRead _fornecedorPartidoRepositoryRead;

        public TSEService(IDoacaoCandidatoRepositoryRead doocaocandidatorepositoryRead, IDoacaoGeralRepositoryRead doacaoGeralRepositoryRead, IDoacaoPartidoGeralRepositoryRead doacaoPartidoGeralRepositoryRead, IDoacaoPartidoRepositoryRead doacaoPartidoRepositoryRead, IFornecedorCandidatoRepositoryRead fornecedorCandidatoRepositoryRead, IFornecedorPartidoRepositoryRead fornecedorPartidoRepositoryRead)
        {
            _doocaocandidatorepositoryRead = doocaocandidatorepositoryRead;
            _doacaoGeralRepositoryRead = doacaoGeralRepositoryRead;
            _doacaoPartidoGeralRepositoryRead = doacaoPartidoGeralRepositoryRead;
            _doacaoPartidoRepositoryRead = doacaoPartidoRepositoryRead;
            _fornecedorCandidatoRepositoryRead = fornecedorCandidatoRepositoryRead;
            _fornecedorPartidoRepositoryRead = fornecedorPartidoRepositoryRead;
        }

        public async Task<BuscaTSEDTO> BuscaCNPJ(string cnpj)
        {
            var doacaocandidato = await _doocaocandidatorepositoryRead.BuscarCNPJ(cnpj);
            var doacaopartido = await _doacaoPartidoRepositoryRead.BuscarCNPJ(cnpj);
            var fornecedorpartido = await _fornecedorPartidoRepositoryRead.BuscarCNPJ(cnpj);
            var fornecedorcandidato = await _fornecedorCandidatoRepositoryRead.BuscarCNPJ(cnpj);

            return new BuscaTSEDTO
            {
                DoacaoCandidato = doacaocandidato,
                DoacaoPartido = doacaopartido,
                FornecedorCandidato = fornecedorcandidato,
                FornecedorPartido = fornecedorpartido,
            };

        }

        public async Task<BuscaTSEDTO> BuscarCPF(string cpf)
        {
            var doacaocandidato = await _doocaocandidatorepositoryRead.BuscarCPF(cpf);
            var doacaogeral = await _doacaoGeralRepositoryRead.BuscarCPF(cpf);
            var doacaopartidogeral = await _doacaoPartidoGeralRepositoryRead.BuscarCPF(cpf);
            var doacaopartido = await _doacaoPartidoRepositoryRead.BuscarCPF(cpf);
            var fornecedorpartido = await _fornecedorPartidoRepositoryRead.BuscarCPF(cpf);
            var fornecedorcandidato = await _fornecedorCandidatoRepositoryRead.BuscarCPF(cpf);

            return new BuscaTSEDTO
            {
                DoacaoCandidato = doacaocandidato,
                DoacaoGeral = doacaogeral,
                DoacaoPartido = doacaopartido,
                DoacaoPartidoGeral = doacaopartidogeral,
                FornecedorCandidato = fornecedorcandidato,
                FornecedorPartido = fornecedorpartido,
            }; ;
        }
    }
}
