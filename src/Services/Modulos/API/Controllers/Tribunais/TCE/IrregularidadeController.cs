using Domain.Tribunal.Entidades.TCE;
using Domain.Tribunal.Interfaces.TCE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Tribunais.TCE
{
    [ApiController]
    [Route("api/irregularidade")]
    [Authorize]
    public class IrregularidadeController : Controller
    {
        private readonly IIrregularidadeRepositoryRead _irregularidadeRepositoryread;


        public IrregularidadeController(IIrregularidadeRepositoryRead irregularidadeRepository)
        {
            _irregularidadeRepositoryread = irregularidadeRepository;

        }



        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _irregularidadeRepositoryread.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("consultaid/{id}")]
        public async Task<Irregularidade> BuscaId(Guid id)
        {
            return await _irregularidadeRepositoryread.ObterId(id);
        }



        [HttpGet("consultacpfj/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            var irregularidade = await _irregularidadeRepositoryread.BuscarCPF(cpf);

            if (irregularidade == null || !irregularidade.Any())
            {
                return NotFound("Nenhuma irregularidade encontrado para o CPF fornecido.");
            }

            return Ok(irregularidade);
        }



    }
}
