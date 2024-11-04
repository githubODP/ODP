using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.Detran
{
    [ApiController]
    [Route("api/veiculoroubo")]
    [Authorize]
    public class VeiculoBloqueioRouboController : Controller
    {
        private readonly IVeiculoBloqueadoRepositoryRead _veiculoBloqueioRouboRepository;

        public VeiculoBloqueioRouboController(IVeiculoBloqueadoRepositoryRead veiculoBloqueioRouboRepository)
        {
            _veiculoBloqueioRouboRepository = veiculoBloqueioRouboRepository;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _veiculoBloqueioRouboRepository.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("consultaid/{id}")]
        public async Task<VeiculoBloqueioRoubo> BuscaId(Guid id)
        {
            return await _veiculoBloqueioRouboRepository.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<VeiculoBloqueioRoubo> SearchCNPJ(string cnpj)
        {
            return await _veiculoBloqueioRouboRepository.BuscarPorCNPJ(cnpj);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<VeiculoBloqueioRoubo> SearchCPF(string cpf)
        {
            return await _veiculoBloqueioRouboRepository.BuscarPorCPF(cpf);
        }
    }
}
