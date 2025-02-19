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
    [Route("api/contratrosinterno")]
    [Authorize]
    public class ContratosInternosController : Controller
    {
        private readonly IContratosInternosRepository _contratosInternosRepository;
        private readonly IContratosInternoRepositoryRead _contratosInternoRepositoryRead;

        public  ContratosInternosController (IContratosInternoRepositoryRead contratosInternoRepositoryRead, IContratosInternosRepository contratosInternosRepository)
        {
            _contratosInternoRepositoryRead = contratosInternoRepositoryRead;
            _contratosInternosRepository = contratosInternosRepository;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var pagedResult = await _contratosInternoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }


        [HttpPost("adicionar")]
        public async Task<IActionResult> adicionar(ContratosInternos contratos)
        {
            try
            {
                await _contratosInternosRepository.Adicionar(contratos);
                return Ok(contratos);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("alterar")]
        public async Task<IActionResult> Alterar(ContratosInternos contratos)
        {
            try
            {
                await _contratosInternosRepository.Atualizar(contratos);

                
                var Atualizado = await _contratosInternoRepositoryRead.ObterId(contratos.Id);

                return Ok(Atualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("excluir")]
        public async Task<IActionResult> Excluir([FromBody] ContratosInternos contratos)
        {
            if (contratos == null)
                return BadRequest("O contrato não pode ser nulo.");

            try
            {
                await _contratosInternosRepository.Deletar(contratos);

                // Retorna o próprio termo excluído
                return Ok(contratos);
            }
            catch (Exception ex)
            {
                // Caso ocorra erro, retorna um termo padrão
                var contratoErro = new ContratosInternos
                {
                        Id = Guid.NewGuid(),
                        Contrato = "Erro ao excluir",
                        NroContrato = "Erro ao excluir",
                        InicioContrato = DateTime.MinValue,
                        FimContrato = DateTime.MinValue,
                        Protocolo = "Erro ao excluir",
                        Valor = 0.00F,
                        Objeto = "Erro ao excluir",
                        Gestor = "Erro ao excluir",
                    //Fiscal = "Erro ao excluir",
                    Dioe = 0,
                        DataPublicacao = DateTime.MinValue,
                        Status = ETipoStatus.SELECIONE
                };

                return StatusCode(500, contratoErro);
            }
        }

        [HttpGet("obterid/{id}")]
        public async Task<IActionResult> ObterID(Guid id)
        {
            return Ok(await _contratosInternoRepositoryRead.ObterId(id));
        }
    }
}
