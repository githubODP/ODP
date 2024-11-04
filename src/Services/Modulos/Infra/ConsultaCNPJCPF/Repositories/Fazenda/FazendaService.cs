

using Domain.ConsultaCNPJCPF.DTO.Fazenda;
using Domain.ConsultaCNPJCPF.InterfacesDTO;
using Domain.Fazenda.Interfaces;

namespace Infra.ConsultaCNPJCPF.Repositories.Fazenda
{
    public class FazendaService : IFazendaService
    {
        private readonly IJuceparRepositoryRead _juceparRepository;
        private readonly INFEletronicaFederalRepositoryRead _nfeletronicafederalRepository;
        private readonly INFEletronicaRepositoryRead _nfeletronicaRepository;

        public FazendaService(IJuceparRepositoryRead juceparRepository,
                               INFEletronicaFederalRepositoryRead nfeletronicafederalRepository,
                               INFEletronicaRepositoryRead nfeletronicaRepository)
        {
            _juceparRepository = juceparRepository;
            _nfeletronicafederalRepository = nfeletronicafederalRepository;
            _nfeletronicaRepository = nfeletronicaRepository;
        }

        public async Task<BuscaFazendaDTO> BuscarCNPJ(string cnpj)
        {
            var jucepar = await _juceparRepository.BuscarCNPJ(cnpj);
            var notafederal = await _nfeletronicafederalRepository.BuscarCNPJ(cnpj);
            var notaestadual = await _nfeletronicaRepository.BuscarCNPJ(cnpj);

            return new BuscaFazendaDTO
            {
                Jucepar = jucepar,
                NFEletronica = notaestadual,
                NFEletronicaFederal = notafederal,
            };
        }

        public async Task<BuscaFazendaDTO> BuscarCPF(string cpf)
        {
            var jucepar = await _juceparRepository.BuscarCPF(cpf);
            var notafederal = await _nfeletronicafederalRepository.BuscarCPF(cpf);


            return new BuscaFazendaDTO
            {
                Jucepar = jucepar,
                NFEletronicaFederal = notafederal,
            };
        }
    }
}
