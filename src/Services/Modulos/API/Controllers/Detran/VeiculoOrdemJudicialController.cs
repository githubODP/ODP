using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.Detran
{
    [ApiController]
    [Route("api/veiculojudicial")]
    [Authorize]
    public class VeiculoOrdemJudicialController : Controller
    {
        private readonly IVeiculoOrdemJudicialRepositoryRead _veiculoOrdemJudicialRepository;

        public VeiculoOrdemJudicialController(IVeiculoOrdemJudicialRepositoryRead veiculoOrdemJudicialRepository)
        {
            _veiculoOrdemJudicialRepository = veiculoOrdemJudicialRepository;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _veiculoOrdemJudicialRepository.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<VeiculoOrdemJudicial> BuscaId(Guid id)
        {
            return await _veiculoOrdemJudicialRepository.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<VeiculoOrdemJudicial> SearchCNPJ(string cnpj)
        {
            return await _veiculoOrdemJudicialRepository.BuscarPorCNPJ(cnpj);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<VeiculoOrdemJudicial> SearchCPF(string cpf)
        {
            return await _veiculoOrdemJudicialRepository.BuscarPorCPF(cpf);
        }
    }
}
