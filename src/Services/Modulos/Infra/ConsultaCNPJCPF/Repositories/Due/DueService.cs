
using Domain.ConsultaCNPJCPF.DTO.DueDiligence;
using Domain.ConsultaCNPJCPF.InterfacesDTO;
using Domain.DueDiligence.Interfaces;

namespace Infra.ConsultaCNPJCPF.Repositories.Due
{
    public class DueService : IDueService
    {
        private readonly IDueRepositoryRead _dueRepositoryRead;

        public DueService(IDueRepositoryRead dueRepositoryRead)
        {
            _dueRepositoryRead = dueRepositoryRead;
        }

        public async Task<BuscaDueDTO> BuscarCPF(string cpf)
        {
            var due = await _dueRepositoryRead.BuscarPorCPF(cpf);

            return new BuscaDueDTO
            {
                Comissionado = due
            };
        }


    }
}
