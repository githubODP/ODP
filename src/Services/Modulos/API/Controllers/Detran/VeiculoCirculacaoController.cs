using Domain.Detran.Entidades;
using Domain.Detran.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.Detran
{
    [ApiController]
    [Route("api/veiculocirculacao")]
    [Authorize]
    public class VeiculoCirculacaoController : Controller
    {
        private readonly IVeiculoCirculacaoRepositoryRead _veiculoCirculacaoRepository;


        public VeiculoCirculacaoController(IVeiculoCirculacaoRepositoryRead veiculoCirculacaoRepository)
        {
            _veiculoCirculacaoRepository = veiculoCirculacaoRepository;

        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _veiculoCirculacaoRepository.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpGet("consultaid/{id}")]
        public async Task<VeiculoCirculacao> BuscaId(Guid id)
        {
            return await _veiculoCirculacaoRepository.ObterId(id);
        }

        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<VeiculoCirculacao> SearchCNPJ(string cnpj)
        {
            return await _veiculoCirculacaoRepository.BuscarPorCNPJ(cnpj);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<VeiculoCirculacao> SearchCPF(string cpf)
        {
            return await _veiculoCirculacaoRepository.BuscarPorCPF(cpf);
        }


    }
}
