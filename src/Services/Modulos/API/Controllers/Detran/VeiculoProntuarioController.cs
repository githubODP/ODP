using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace API.Controllers.Detran
{
    [ApiController]
    [Route("api/veiculoprontuario")]
    [Authorize]
    public class VeiculoProntuarioController : Controller
    {
        private readonly IVeiculoProntuarioRepositoryRead _veiculoProntuarioRepository;

        public VeiculoProntuarioController(IVeiculoProntuarioRepositoryRead veiculoProntuarioRepository)
        {
            _veiculoProntuarioRepository = veiculoProntuarioRepository;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _veiculoProntuarioRepository.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<VeiculoProntuario> BuscaId(Guid id)
        {
            return await _veiculoProntuarioRepository.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<VeiculoProntuario> SearchCNPJ(string cnpj)
        {
            return await _veiculoProntuarioRepository.BuscarPorCNPJ(cnpj);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<VeiculoProntuario> SearchCPF(string cpf)
        {
            return await _veiculoProntuarioRepository.BuscarPorCPF(cpf);
        }
    }
}
