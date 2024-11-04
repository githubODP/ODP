
using Domain.ConsultaCNPJCPF.DTO.GovernoFederal;
using Domain.ConsultaCNPJCPF.InterfacesDTO;
using Domain.GovernoFederal.Interfaces;

namespace Infra.ConsultaCNPJCPF.Repositories.GovernoFederal
{
    public class GovernoFederalService : IGovernoFederalService
    {
        private readonly IAeronaveRepositoryRead _aeronaveRepository;
        private readonly IBeneficioContinuoRepositoryRead _beneficiocontinuoRepository;
        private readonly ICeisRepositoryRead _ceisRepository;
        private readonly IExpulsoFederalRepositoryRead _expulsofederalRepository;
        private readonly ISeguroDefesoRepositoryRead _segurodefesoRepository;
        private readonly ITrabalhoEscravoRepositoryRead _trabalhoescravoRepository;
        private readonly IAcordoLenienciaRepositoryRead _acordoLenienciaRepository;
        private readonly IPEPRepositoryRead _pepRepository;
        private readonly IBolsaFamiliaRepositoryRead _bolsaFamiliaRepository;
        private readonly ICepimRepositoryRead _cepimRepository;
        private readonly ICnepRepositoryRead _cnepRepository;

        public GovernoFederalService(IAeronaveRepositoryRead aeronaveRepository,
                                     IBeneficioContinuoRepositoryRead beneficiocontinuoRepository,
                                     ICeisRepositoryRead ceisRepository,
                                     IExpulsoFederalRepositoryRead expulsofederalRepository,
                                     ISeguroDefesoRepositoryRead segurodefesoRepository,
                                     ITrabalhoEscravoRepositoryRead trabalhoescravoRepository,
                                     IAcordoLenienciaRepositoryRead acordoLenienciaRepository,
                                     IPEPRepositoryRead pepRepository,
                                     IBolsaFamiliaRepositoryRead bolsaFamiliaRepository,
                                     ICepimRepositoryRead cepimRepository,
                                     ICnepRepositoryRead cnepRepository)
        {
            _aeronaveRepository = aeronaveRepository;
            _beneficiocontinuoRepository = beneficiocontinuoRepository;
            _ceisRepository = ceisRepository;
            _expulsofederalRepository = expulsofederalRepository;
            _segurodefesoRepository = segurodefesoRepository;
            _trabalhoescravoRepository = trabalhoescravoRepository;
            _acordoLenienciaRepository = acordoLenienciaRepository;
            _pepRepository = pepRepository;
            _bolsaFamiliaRepository = bolsaFamiliaRepository;
            _cepimRepository = cepimRepository;
            _cnepRepository = cnepRepository;
        }

        public async Task<BuscaFederalDTO> BuscarCNPJ(string cnpj)
        {
            var aeronave = await _aeronaveRepository.BuscarCNPJ(cnpj);
            var ceis = await _ceisRepository.BuscarCNPJ(cnpj);
            var trabalhoescravo = await _trabalhoescravoRepository.BuscarCNPJ(cnpj);
            var acordoleniencia = await _acordoLenienciaRepository.BuscarCNPJ(cnpj);
            var cepim = await _cepimRepository.BuscarCNPJ(cnpj);
            var cnep = await _cnepRepository.BuscarCNPJ(cnpj);

            return new BuscaFederalDTO
            {
                Aeronave = aeronave,
                Ceis = ceis,
                TrabalhoEscravo = trabalhoescravo,
                AcordosLeniencia = acordoleniencia,
                Cepim = cepim,
                Cnep = cnep
            };

        }

        public async Task<BuscaFederalDTO> BuscarCPF(string cpf)
        {
            var aeronave = await _aeronaveRepository.BuscarCPF(cpf);
            var beneficio = await _beneficiocontinuoRepository.BuscarCPF(cpf);
            var ceis = await _ceisRepository.BuscarCPF(cpf);
            var expulsofederal = await _expulsofederalRepository.BuscarCPF(cpf);
            var segurodefeso = await _segurodefesoRepository.BuscarCPF(cpf);
            var trabalhoescravo = await _trabalhoescravoRepository.BuscarCPF(cpf);
            var pep = await _pepRepository.BuscarCPF(cpf);
            var bolsa = await _bolsaFamiliaRepository.BuscarCPF(cpf);   
            var cnep = await _cnepRepository.BuscarCPF(cpf);


            return new BuscaFederalDTO
            {
                Aeronave = aeronave,
                BeneficioContinuo = beneficio,
                Ceis = ceis,
                ExpulsoFederal = expulsofederal,
                SeguroDefeso = segurodefeso,
                TrabalhoEscravo = trabalhoescravo,
                PEP = pep,
                BolsaFamilia = bolsa,
                Cnep = cnep,
            };
        }
    }
}
