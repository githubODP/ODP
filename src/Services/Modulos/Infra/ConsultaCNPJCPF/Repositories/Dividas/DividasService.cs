
using Dividas.Domain.Interfaces;
using Domain.ConsultaCNPJCPF.DTO.Dividas;
using Domain.ConsultaCNPJCPF.InterfacesDTO;

namespace Infra.ConsultaCNPJCPF.Repositories.Dividas
{
    public class DividasService : IDividasService
    {
        private readonly IDividaFgtsRepositoryRead _dividaFgtsRepository;
        private readonly IDividaNaoPrevidenciaRepositoryRead _dividanaoprevidenciaRepository;
        private readonly IDividaPrevidenciaRepositoryRead _dividaprevidenciaRepository;


        public DividasService(IDividaFgtsRepositoryRead dividaFgtsRepository,
                              IDividaNaoPrevidenciaRepositoryRead dividanaoprevidenciaRepository,
                              IDividaPrevidenciaRepositoryRead dividaprevidenciaRepository)
        {
            _dividaFgtsRepository = dividaFgtsRepository;
            _dividanaoprevidenciaRepository = dividanaoprevidenciaRepository;
            _dividaprevidenciaRepository = dividaprevidenciaRepository;
        }

        public async Task<BuscaDividasDTO> BuscarCNPJ(string cnpj)
        {
            var dividafgts = await _dividaFgtsRepository.BuscarCNPJ(cnpj);
            var dividanaoprevidencia = await _dividanaoprevidenciaRepository.BuscarCNPJ(cnpj);
            var dividaprevidencia = await _dividaprevidenciaRepository.BuscarCNPJ(cnpj);

            return new BuscaDividasDTO
            {
                DividaFGTS = dividafgts,
                DividaNaoPrevidenciaria = dividanaoprevidencia,
                DividaPrevidenciaria = dividaprevidencia,
            };
        }


        public async Task<BuscaDividasDTO> BuscarCPF(string cpf)
        {
            var dividafgts = await _dividaFgtsRepository.BuscarCPF(cpf);
            var dividanaoprevidencia = await _dividanaoprevidenciaRepository.BuscarCPF(cpf);
            var dividaprevidencia = await _dividaprevidenciaRepository.BuscarCPF(cpf);

            return new BuscaDividasDTO
            {
                DividaFGTS = dividafgts,
                DividaNaoPrevidenciaria = dividanaoprevidencia,
                DividaPrevidenciaria = dividaprevidencia,
            };
        }
    }
}
