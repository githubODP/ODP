
using Domain.RecursosHumanos.Entidades;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RH.API.Controllers
{
    [ApiController]
    [Route("api/dependentes")]
    [Authorize]
    public class DependentesController : Controller
    {
        private readonly IDependenteRepositoryRead _dependentesRepositoryRead;


        public DependentesController(IDependenteRepositoryRead repositoryRead)
        {

            _dependentesRepositoryRead = repositoryRead;
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<Dependente> SearchCPF(string cpf)
        {
            return await _dependentesRepositoryRead.BuscarPorCPF(cpf);
        }



    }
}
