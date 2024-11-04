using Domain.ConsultaCNPJCPF.DTO.Tribunais.TCU;
using Domain.ConsultaCNPJCPF.InterfacesDTO;
using Domain.Tribunal.Interfaces.TCU;


namespace Infra.ConsultaCNPJCPF.Repositories.Tribunal
{
    public class TCUService : ITCUService
    {
        private readonly IContaEleitoralIrregularRepositoryRead _contaEleitoralIrregularRepository;
        private readonly IContaIrregularRepositoryRead _contairregularRepository;
        private readonly IInabilitadoRepositoryRead _inabilitadoRepository;
        private readonly IInidoneoRepositoryRead _inidoneoRepository;

        public TCUService
            (
            IContaEleitoralIrregularRepositoryRead contaeleitoralirregularRepository,
            IContaIrregularRepositoryRead contairregularRepository,
            IInabilitadoRepositoryRead inabilitadoRepository,
            IInidoneoRepositoryRead inidoneoRepository
            )
        {

            _contaEleitoralIrregularRepository = contaeleitoralirregularRepository;
            _contairregularRepository = contairregularRepository;
            _inabilitadoRepository = inabilitadoRepository;
            _inidoneoRepository = inidoneoRepository;

        }

        public async Task<BuscaTCUDTO> BuscaCNPJ(string cnpj)
        {

            var contairregular = await _contairregularRepository.BuscarCNPJ(cnpj);
            var inidoneo = await _inidoneoRepository.BuscarCNPJ(cnpj);

            return new BuscaTCUDTO
            {
                ContaIrregular = contairregular,
                Inidoneo = inidoneo,
            };

        }

        public async Task<BuscaTCUDTO> BuscarCPF(string cpf)
        {

            var contairregular = await _contairregularRepository.BuscarCPF(cpf);
            var inabilitado = await _inabilitadoRepository.BuscarCPF(cpf);
            var contaeleitoral = await _contaEleitoralIrregularRepository.BuscarCPF(cpf);


            return new BuscaTCUDTO
            {
                Inabilitado = inabilitado,
                ContaIrregular = contairregular,
                ContaEleitoralIrregular = contaeleitoral,
            };
        }
    }
}
