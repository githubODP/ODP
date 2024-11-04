using Domain.Compras.Interfaces;
using Domain.ConsultaCNPJCPF.DTO.Compras;
using Domain.ConsultaCNPJCPF.InterfacesDTO;

namespace Infra.ConsultaCNPJCPF.Repositories.Compras
{
    public class ComprasService : IComprasService
    {
        private readonly IContratoRepositoryRead _contratoRepository;
        private readonly IDispensaRepositoryRead _dispensaRepository;
        private readonly ILicitacaoRepositoryRead _licitacaoRepository;

        public ComprasService(
             IContratoRepositoryRead contratoRepository,
             IDispensaRepositoryRead dispensaRepository,
             ILicitacaoRepositoryRead licitacaoRepository)
        {
            _contratoRepository = contratoRepository;
            _dispensaRepository = dispensaRepository;
            _licitacaoRepository = licitacaoRepository;
        }

        public async Task<BuscaComprasDTO> BuscarCNPJ(string cnpj)
        {
            var contrato = await _contratoRepository.BuscarCNPJ(cnpj);
            var dispensa = await _dispensaRepository.BuscarCNPJ(cnpj);
            var licitacao = await _licitacaoRepository.BuscarCNPJ(cnpj);

            return new BuscaComprasDTO
            {
                Contrato = contrato,
                Dispensa = dispensa,
                Licitacao = licitacao,
            };
        }

        public async Task<BuscaComprasDTO> BuscarCPF(string cpf)
        {
            var contrato = await _contratoRepository.BuscarCPF(cpf);



            return new BuscaComprasDTO
            {
                Contrato = contrato,


            };
        }

    }
}
