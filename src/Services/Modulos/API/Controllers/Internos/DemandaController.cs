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
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string termo = null)
        {
            try
            {
                // Validação dos parâmetros
                if (pageNumber < 1 || pageSize < 1)
                {
                    return BadRequest("O número da página e o tamanho da página devem ser maiores que zero.");
                }

                termo = string.IsNullOrWhiteSpace(termo) ? null : termo.Trim();

                var pagedResult = await _demandaRepositoryRead.Listar(pageNumber, pageSize, termo);
                return Ok(pagedResult);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocorreu um erro ao processar sua solicitação.");
            }
        }




        [HttpGet("obterid/{id}")]
        public async Task<IActionResult> ObterID(Guid id)
        {
            try
            {
                // Validação do ID
                if (id == Guid.Empty)
                {
                    return BadRequest("O ID fornecido é inválido.");
                }

                var termo = await _demandaRepositoryRead.ObterId(id);

                if (termo == null)
                {
                    return NotFound("Demanda não encontrado.");
                }

                return Ok(termo);
            }
            catch (Exception ex)
            {


                return StatusCode(500, "Ocorreu um erro ao processar sua solicitação.");
            }
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
        public async Task<IActionResult> Adicionar([FromBody] DemandasInternas termo)
        {
            if (termo == null)
            {
                return BadRequest("Dados inválidos.");
            }

            await _demandaRepository.Adicionar(termo);
            return CreatedAtAction(nameof(ObterID), new { id = termo.Id }, termo);
        }


        [HttpPost("alterar/{id}")]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] DemandasInternas termo)
        {
            try
            {
                if (termo == null || id == Guid.Empty)
                {
                    return BadRequest("Dados inválidos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (termo.Id != id)
                {
                    return BadRequest("O ID da Demanda não corresponde ao ID da rota.");
                }

                await _demandaRepository.Atualizar(termo);

                return Ok(termo);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao processar sua solicitação.");
            }
        }




        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var termoExistente = await _demandaRepositoryRead.ObterId(id);
            if (termoExistente == null)
                return NotFound("Termo de cooperação não encontrado.");

            await _demandaRepository.Deletar(termoExistente);
            return Ok("Termo excluído com sucesso.");
        }

    }
}
