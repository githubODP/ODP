
using Domain.ConsultaCNPJCPF.DTO.RecursosHumanos;
using Domain.ConsultaCNPJCPF.InterfacesDTO;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;

namespace Infra.ConsultaCNPJCPF.Repositories.RH
{
    public class RHService : IRHService
    {
        private readonly IDependenteRepositoryRead _dependenteRepository;
        private readonly IFuncionarioRepositoryRead _funcionarioRepository;

        public RHService(IDependenteRepositoryRead dependenteRepository,
                         IFuncionarioRepositoryRead funcionarioRepository)
        {
            _dependenteRepository = dependenteRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<BuscaRHDTO> BuscaCPF(string cpf)
        {
            var dependente = await _dependenteRepository.BuscarPorCPF(cpf);
            var funcionario = await _funcionarioRepository.BuscarPorCPF(cpf);

            return new BuscaRHDTO
            {
                Funcionario = funcionario,
                Dependente = dependente,
            };
        }


    }
}
