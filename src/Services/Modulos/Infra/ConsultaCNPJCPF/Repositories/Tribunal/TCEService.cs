
using Domain.ConsultaCNPJCPF.DTO.Tribunais.TCE;
using Domain.ConsultaCNPJCPF.InterfacesDTO;
using Domain.Tribunal.Interfaces.TCE;

namespace Infra.ConsultaCNPJCPF.Repositories.Tribunal
{
    public class TCEService : ITCEService
    {
        private readonly ICNPJRestritoRepositoryRead _cnpjrestritoRepository;
        private readonly ICPFRestritoRepositoryRead _cpfrestritoRepository;
        private readonly IInadimplenteRepositoryRead _inadimplenteRepository;
        private readonly IIrregularidadeRepositoryRead _irregularidadeRepository;

        public TCEService(ICNPJRestritoRepositoryRead cnpjrestritoRepository,
                          ICPFRestritoRepositoryRead cpfrestritoRepository,
                          IInadimplenteRepositoryRead inadimplenteRepository,
                          IIrregularidadeRepositoryRead irregularidadeRepository
                          )
        {
            _cnpjrestritoRepository = cnpjrestritoRepository;
            _cpfrestritoRepository = cpfrestritoRepository;
            _inadimplenteRepository = inadimplenteRepository;
            _irregularidadeRepository = irregularidadeRepository;
        }

        public async Task<BuscaTCEDTO> BuscaCNPJ(string cnpj)
        {
            var cnpjrestrito = await _cnpjrestritoRepository.BuscarCNPJ(cnpj);
            var inadimplente = await _inadimplenteRepository.BuscarCNPJ(cnpj);


            return new BuscaTCEDTO
            {
                CNPJRestrito = cnpjrestrito,
                Inadimplente = inadimplente,

            };
        }

        public async Task<BuscaTCEDTO> BuscarCPF(string cpf)
        {

            var cpfrestrito = await _cpfrestritoRepository.BuscarCPF(cpf);
            var inadimplente = await _inadimplenteRepository.BuscarCPF(cpf);
            var irregular = await _irregularidadeRepository.BuscarCPF(cpf);

            return new BuscaTCEDTO
            {
                Irregularidade = irregular,
                Inadimplente = inadimplente,
                CPFRestrito = cpfrestrito,
            };

        }
    }
}
