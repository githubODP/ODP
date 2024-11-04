using Domain.Tribunal.Entidades.TCU;
using Domain.Tribunal.Interfaces.TCU;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Tribunais.TCU
{

    [ApiController]
    [Route("api/contaeleitoralirregular")]
    [Authorize]
    public class ContaEleitoralIrregularController : Controller
    {
        private readonly IContaEleitoralIrregularRepositoryRead _contaEleitoralIrregularRepositoryread;

        public ContaEleitoralIrregularController(IContaEleitoralIrregularRepositoryRead contaEleitoralIrregularRepository)
        {
            _contaEleitoralIrregularRepositoryread = contaEleitoralIrregularRepository;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _contaEleitoralIrregularRepositoryread.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<ContaEleitoralIrregular> BuscaId(Guid id)
        {
            return await _contaEleitoralIrregularRepositoryread.ObterId(id);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarCPF(string cpf)
        {
            var conta = await _contaEleitoralIrregularRepositoryread.BuscarCPF(cpf);

            if (conta == null || !conta.Any())
            {
                return NotFound("Nenhuma conta encontrada para o CPF fornecido.");
            }

            return Ok(conta);
        }





    }
}
