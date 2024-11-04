using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace API.Controllers.Detran
{
    [ApiController]
    [Route("api/veiculoregistro")]
    [Authorize]
    public class VeiculoRegistroForaController : Controller
    {
        private readonly IVeiculoRegistroForaRepositoryRead _veiculoRegistroForaRepository;

        public VeiculoRegistroForaController(IVeiculoRegistroForaRepositoryRead veiculoRegistroForaRepository)
        {
            _veiculoRegistroForaRepository = veiculoRegistroForaRepository;
        }
        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _veiculoRegistroForaRepository.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpGet("consultaid/{id}")]
        public async Task<VeiculoRegistroFora> BuscaId(Guid id)
        {
            return await _veiculoRegistroForaRepository.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<VeiculoRegistroFora> SearchCNPJ(string cnpj)
        {
            return await _veiculoRegistroForaRepository.BuscarPorCNPJ(cnpj);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<VeiculoRegistroFora> SearchCPF(string cpf)
        {
            return await _veiculoRegistroForaRepository.BuscarPorCPF(cpf);
        }
    }
}
