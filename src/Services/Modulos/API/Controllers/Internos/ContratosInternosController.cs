using Domain.Internos.Entidade;
using Domain.Internos.Enum;
using Domain.Internos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.Internos
{
    [ApiController]
    [Route("api/contratosinternos")]
    [Authorize]

    public class ContratosInternosController : Controller
    {
        private readonly IContratosInternosRepository _contratosInternosRepository;
        private readonly IContratosInternosRepositoryRead _contratosInternoRepositoryRead;

        public ContratosInternosController(IContratosInternosRepositoryRead contratosInternoRepositoryRead, IContratosInternosRepository contratosInternosRepository)
        {
            _contratosInternoRepositoryRead = contratosInternoRepositoryRead;
            _contratosInternosRepository = contratosInternosRepository;
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

                var pagedResult = await _contratosInternoRepositoryRead.Listar(pageNumber, pageSize, termo);
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

                var termo = await _contratosInternoRepositoryRead.ObterId(id);

                if (termo == null)
                {
                    return NotFound("Contrato não encontrado.");
                }

                return Ok(termo);
            }
            catch (Exception ex)
            {


                return StatusCode(500, "Ocorreu um erro ao processar sua solicitação.");
            }
        }


       

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] ContratosInternos contratos)
        {
            if (contratos == null)
            {
                return BadRequest("Dados inválidos.");
            }

            await _contratosInternosRepository.Adicionar(contratos);
            return CreatedAtAction(nameof(ObterID), new { id = contratos.Id }, contratos);
        }


        [HttpPost("alterar/{id}")]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] ContratosInternos contratos)
        {
            try
            {
                if (contratos == null || id == Guid.Empty)
                {
                    return BadRequest("Dados inválidos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (contratos.Id != id)
                {
                    return BadRequest("O ID do Contrato não corresponde ao ID da rota.");
                }

                await _contratosInternosRepository.Atualizar(contratos);

                return Ok(contratos);
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
            var contratoExistente = await _contratosInternoRepositoryRead.ObterId(id);
            if (contratoExistente == null)
                return NotFound("Termo de cooperação não encontrado.");

            await _contratosInternosRepository.Deletar(contratoExistente);
            return Ok("Termo excluído com sucesso.");
        }


    }
}
