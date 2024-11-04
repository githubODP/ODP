
using Domain.ConsultaCNPJCPF.DTO.Detran;
using Domain.ConsultaCNPJCPF.InterfacesDTO;
using Domain.Detran.Interfaces;

namespace Infra.ConsultaCNPJCPF.Repositories.Detran
{
    public class DetranService : IDetranService
    {
        private readonly IVeiculoBaixadoRepositoryRead _veiculobaixadoRepository;
        private readonly IVeiculoBloqueadoRepositoryRead _veiculobloqueadoRepository;
        private readonly IVeiculoCirculacaoRepositoryRead _veiculoCirculacaoRepository;
        private readonly IVeiculoIndisponivelRepositoryRead _veiculoindisponivelRepository;
        private readonly IVeiculoOrdemJudicialRepositoryRead _veiculoOrdemJudicialRepository;
        private readonly IVeiculoProntuarioRepositoryRead _veiculoprontuarioRepository;
        private readonly IVeiculoRegistroForaRepositoryRead _veiculoRegistoForaRepository;

        public DetranService(IVeiculoBaixadoRepositoryRead veiculobaixadoRepository,
               IVeiculoBloqueadoRepositoryRead veiculobloqueadoRepository,
               IVeiculoCirculacaoRepositoryRead veiculoCirculacaoRepository,
               IVeiculoIndisponivelRepositoryRead veiculoindisponivelRepository,
               IVeiculoOrdemJudicialRepositoryRead veiculoOrdemJudicialRepository,
               IVeiculoProntuarioRepositoryRead veiculoprontuarioRepository,
               IVeiculoRegistroForaRepositoryRead veiculoRegistoForaRepository)
        {
            _veiculobaixadoRepository = veiculobaixadoRepository;
            _veiculobloqueadoRepository = veiculobloqueadoRepository;
            _veiculoCirculacaoRepository = veiculoCirculacaoRepository;
            _veiculoindisponivelRepository = veiculoindisponivelRepository;
            _veiculoOrdemJudicialRepository = veiculoOrdemJudicialRepository;
            _veiculoprontuarioRepository = veiculoprontuarioRepository;
            _veiculoRegistoForaRepository = veiculoRegistoForaRepository;
        }

        public async Task<BuscaDetranDTO> BuscarCNPJ(string cnpj)
        {
            var veiculobaixado = await _veiculobaixadoRepository.BuscarPorCNPJ(cnpj);
            var veiculobloqueado = await _veiculobloqueadoRepository.BuscarPorCNPJ(cnpj);
            var veiculocirculacao = await _veiculoCirculacaoRepository.BuscarPorCNPJ(cnpj);
            var veiculoIndiponivel = await _veiculoindisponivelRepository.BuscarPorCNPJ(cnpj);
            var veiculoJudicial = await _veiculoOrdemJudicialRepository.BuscarPorCNPJ(cnpj);
            var veiculoprontuario = await _veiculoprontuarioRepository.BuscarPorCNPJ(cnpj);
            var veiculoregistro = await _veiculoRegistoForaRepository.BuscarPorCNPJ(cnpj);

            return new BuscaDetranDTO
            {
                VeiculoBaixado = veiculobaixado,
                VeiculoBloqueioRoubo = veiculobloqueado,
                VeiculoCirculacao = veiculocirculacao,
                VeiculoIndispAdmin = veiculoIndiponivel,
                VeiculoOrdemJudicial = veiculoJudicial,
                VeiculoProntuario = veiculoprontuario,
                VeiculoRegistroFora = veiculoregistro,
            };

        }

        public async Task<BuscaDetranDTO> BuscarCPF(string cpf)
        {
            var veiculobaixado = await _veiculobaixadoRepository.BuscarPorCPF(cpf);
            var veiculobloqueado = await _veiculobloqueadoRepository.BuscarPorCPF(cpf);
            var veiculocirculacao = await _veiculoCirculacaoRepository.BuscarPorCPF(cpf);
            var veiculoIndiponivel = await _veiculoindisponivelRepository.BuscarPorCPF(cpf);
            var veiculoJudicial = await _veiculoOrdemJudicialRepository.BuscarPorCPF(cpf);
            var veiculoprontuario = await _veiculoprontuarioRepository.BuscarPorCPF(cpf);
            var veiculoregistro = await _veiculoRegistoForaRepository.BuscarPorCPF(cpf);

            return new BuscaDetranDTO
            {
                VeiculoBaixado = veiculobaixado,
                VeiculoBloqueioRoubo = veiculobloqueado,
                VeiculoCirculacao = veiculocirculacao,
                VeiculoIndispAdmin = veiculoIndiponivel,
                VeiculoOrdemJudicial = veiculoJudicial,
                VeiculoProntuario = veiculoprontuario,
                VeiculoRegistroFora = veiculoregistro,
            };
        }
    }
}
