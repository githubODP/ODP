using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Internos
{
    [ApiController]
    [Route("api/demanda")]
    [Authorize]
    public class DemandaController : Controller
    {
        private readonly IDemandaRepository _demandaRepository;
        private readonly IDemandaRepositoryRead _demandaRepositoryRead;

        public DemandaController(IDemandaRepositoryRead demandaRepositoryRead, IDemandaRepository demandaRepository)
        {
            _demandaRepositoryRead = demandaRepositoryRead;
            _demandaRepository = demandaRepository;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _demandaRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }




        [HttpGet("buscaId/{id}")]
        public async Task<DemandasInternas> BuscaId(Guid id)
        {
            return await _demandaRepositoryRead.ObterId(id);
        }



        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> BuscarPorCNPJ(string cnpj)
        {
            var demanda = await _demandaRepositoryRead.BuscarCNPJ(cnpj);

            if (demanda == null || !demanda.Any())
            {
                return NotFound("Nenhum contrato encontrado para o CNPJ fornecido.");
            }

            return Ok(demanda);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var demanda = await _demandaRepositoryRead.BuscarCPF(cpf);

            if (demanda == null || !demanda.Any())
            {
                return NotFound("Nenhum contrato encontrado para o CPF fornecido.");
            }

            return Ok(demanda);
        }

        [HttpPost("adicionar")]

        public async Task Add(DemandasInternas demandas)
        {
            await _demandaRepository.Adicionar(demandas);
        }


        [HttpPut("alterar")]

        public async Task Update(DemandasInternas demandas)
        {
            await _demandaRepository.Atualizar(demandas);
        }

        [HttpDelete("deletar/{id}")]
        public async Task RemoveInstauracao(Guid id)
        {
            var remover = await _demandaRepositoryRead.ObterId(id);
            await _demandaRepository.Deletar(remover);

        }
    }
}
