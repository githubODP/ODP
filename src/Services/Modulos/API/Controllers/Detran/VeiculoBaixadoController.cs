using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.Detran
{
    [ApiController]
    [Route("api/veiculobaixado")]
    [Authorize]
    public class VeiculoBaixadoController : Controller
    {
        private readonly IVeiculoBaixadoRepositoryRead _veiculoBaixadoRepository;

        public VeiculoBaixadoController(IVeiculoBaixadoRepositoryRead veiculoBaixadoRepository)
        {
            _veiculoBaixadoRepository = veiculoBaixadoRepository;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _veiculoBaixadoRepository.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("consultaid/{id}")]
        public async Task<VeiculoBaixado> BuscaId(Guid id)
        {
            return await _veiculoBaixadoRepository.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<VeiculoBaixado> SearchCNPJ(string cnpj)
        {
            return await _veiculoBaixadoRepository.BuscarPorCNPJ(cnpj);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<VeiculoBaixado> SearchCPF(string cpf)
        {
            return await _veiculoBaixadoRepository.BuscarPorCPF(cpf);
        }
    }
}
