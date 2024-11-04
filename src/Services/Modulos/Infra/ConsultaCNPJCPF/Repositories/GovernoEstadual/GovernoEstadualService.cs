
using Domain.ConsultaCNPJCPF.DTO.GovernoEstadual;
using Domain.ConsultaCNPJCPF.InterfacesDTO;
using Domain.GovernoEstadual.Interfaces;

namespace Infra.ConsultaCNPJCPF.Repositories.GovernoEstadual
{
    public class GovernoEstadualService : IGovernoEstadualService
    {
        private readonly IPADVRepositoryRead _padvRepository;
        private readonly IAmbientalRepositoryRead _ambientalRepositoryRead;

        public GovernoEstadualService(IPADVRepositoryRead padvRepository,
                                      IAmbientalRepositoryRead ambientalRepositoryRead)
        {
            _padvRepository = padvRepository;
            _ambientalRepositoryRead = ambientalRepositoryRead;
        }

        public async Task<BuscaEstadualDTO> BuscarCNPJ(string cnpj)
        {
            var padv = await _padvRepository.BuscarCNPJ(cnpj);
            var ambiental = await _ambientalRepositoryRead.BuscarCNPJ(cnpj);

            return new BuscaEstadualDTO
            {
                Ambiental = ambiental,
                PADV = padv,
            };

        }



        public async Task<BuscaEstadualDTO> BuscarCPF(string cpf)
        {

            var ambiental = await _ambientalRepositoryRead.BuscarCPF(cpf);

            return new BuscaEstadualDTO
            {
                Ambiental = ambiental,

            };
        }
    }
}
