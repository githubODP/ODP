using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.Detran
{
    [ApiController]
    [Route("api/veiculoindisAdmin")]
    [Authorize]
    public class VeiculoIndispAdminController : Controller
    {
        private readonly IVeiculoIndisponivelRepositoryRead _veiculoIndispAdminRepository;

        public VeiculoIndispAdminController(IVeiculoIndisponivelRepositoryRead veiculoIndispAdminRepository)
        {
            _veiculoIndispAdminRepository = veiculoIndispAdminRepository;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _veiculoIndispAdminRepository.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("consultaid/{id}")]
        public async Task<VeiculoIndispAdmin> BuscaId(Guid id)
        {
            return await _veiculoIndispAdminRepository.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<VeiculoIndispAdmin> SearchCNPJ(string cnpj)
        {
            return await _veiculoIndispAdminRepository.BuscarPorCNPJ(cnpj);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<VeiculoIndispAdmin> SearchCPF(string cpf)
        {
            return await _veiculoIndispAdminRepository.BuscarPorCPF(cpf);
        }
    }
}
