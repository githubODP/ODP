using CGEODP.Core.DomainObjects;
using Domain.DueDiligence.Entidade;
using Domain.DueDiligence.Interfaces;
using Infra.DueDiligence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.DueAnalise
{
    [ApiController]
    [Route("api/analise")]
    [Authorize]
    public class AnaliseController : Controller
    {


        private readonly IAnaliseRepository _analiseRepository;
        private readonly IAnaliseRepositoryRead _analiseRepositoryRead;
        private readonly IDueRepositoryRead _dueRepositoryRead;
        private readonly ILogger<AnaliseController> _logger;


        public AnaliseController(IAnaliseRepository analiseRepository, IAnaliseRepositoryRead analiseRepositoryRead, IDueRepositoryRead dueRepositoryRead, ILogger<AnaliseController> logger)
        {
            _analiseRepository = analiseRepository;
            _analiseRepositoryRead = analiseRepositoryRead;
           _dueRepositoryRead = dueRepositoryRead;
            _logger = logger;
        }

        [HttpGet("listaradicionais")]
        public async Task<IActionResult> ListarComDadosAdicionais([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest("Número da página e tamanho da página devem ser maiores que zero.");

            var resultado = await _analiseRepositoryRead.ListarDadosAdicionais(pageNumber, pageSize);

            if (resultado.Results == null || !resultado.Results.Any())
                return NotFound("Nenhuma análise encontrada.");

            return Ok(resultado);
        }


        [HttpGet("obter/{id:guid}")]
        public async Task<IActionResult> ObterPorId([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("O ID não pode ser vazio.");

            var analise = await _analiseRepositoryRead.ObterId(id);

            if (analise == null)
                return NotFound($"Nenhuma análise encontrada com o ID {id}.");

            return Ok(analise);
        }

        [HttpGet("pesquisar-comissionado/{nroProtocolo}")]
        public async Task<IActionResult> PesquisarComissionado(string nroProtocolo)
        {
            if (string.IsNullOrEmpty(nroProtocolo))
                return BadRequest(new { sucesso = false, mensagem = "Número do protocolo deve ser informado." });

            var resultado = await _dueRepositoryRead.ObterPorProtocolo(nroProtocolo);

            if (resultado == null)
                return NotFound(new { sucesso = false, mensagem = $"Nenhum comissionado encontrado com o número de protocolo {nroProtocolo}." });

            return Ok(new
            {
                resultado.NroProtocolo,
                resultado.Nome,
                resultado.CPF,
                resultado.Orgao,
                resultado.Observacao
            });
        }



        [HttpPost("adicionar")]
        public async Task<IActionResult> Criar([FromBody] AnaliseCreateDTO dto)
        {
           

            if (dto == null)
                return BadRequest("Os dados da análise são obrigatórios.");

            if (string.IsNullOrWhiteSpace(dto.NroProtocolo))
                return BadRequest("O número do protocolo deve ser informado.");

            // Buscar o comissionado pelo NroProtocolo
            var comissionado = await _dueRepositoryRead.ObterPorProtocolo(dto.NroProtocolo);

            if (comissionado == null)
                return NotFound($"Nenhum comissionado encontrado com o número de protocolo {dto.NroProtocolo}.");

            // Obter o e-mail do usuário logado
            var emailUsuarioLogado = HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (string.IsNullOrEmpty(emailUsuarioLogado))
            {
                return Unauthorized("O usuário logado não possui um e-mail válido.");
            }

            // Criar a nova análise
            var analise = new Analise
            {
                DataAnalise = dto.DataAnalise,
                AnaliseTecnica = dto.AnaliseTecnica,
                Risco = dto.Risco,
                Ressalvas = dto.Ressalvas,
                Responsavel = emailUsuarioLogado,
                ComissionadoId = comissionado.Id // Vincula ao comissionado encontrado
            };

            await _analiseRepository.Adicionar(analise);

            // Retornar os dados no AnaliseResponseDTO
            var responseDto = new AnaliseResponseDTO
            {
                Id = analise.Id,
                NroProtocolo = dto.NroProtocolo,
                DataAnalise = analise.DataAnalise,
                AnaliseTecnica = analise.AnaliseTecnica,
                Risco = analise.Risco.ToString(),
                Ressalvas = analise.Ressalvas.ToString(),
                Responsavel = analise.Responsavel,
                Nome = comissionado.Nome,
                CPF = comissionado.CPF,
                Orgao = comissionado.Orgao,
                Observacao = comissionado.Observacao
            };

            return CreatedAtAction(nameof(ObterPorId), new { id = analise.Id }, responseDto);
        }



        [HttpPut("alterar")]
        public async Task<IActionResult> Alterar([FromBody] AnaliseUpdateDTO dto)
        {
            if (dto == null)
                return BadRequest("Os dados da análise são obrigatórios.");

            if (dto.Id == Guid.Empty)
                return BadRequest("O ID da análise deve ser informado.");

            var analise = await _analiseRepositoryRead.ObterId(dto.Id);

            if (analise == null)
                return NotFound($"Nenhuma análise encontrada com o ID {dto.Id}.");

            // Obter o e-mail do usuário logado
            var emailUsuarioLogado = HttpContext.User?.Claims
                .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;

            if (string.IsNullOrWhiteSpace(emailUsuarioLogado))
                return Unauthorized("O usuário logado não possui um e-mail válido.");

            // Atualizar os dados da análise
            analise.DataAnalise = dto.DataAnalise;
            analise.AnaliseTecnica = dto.AnaliseTecnica;
            analise.Risco = dto.Risco;
            analise.Ressalvas = dto.Ressalvas;
            analise.Responsavel = emailUsuarioLogado;

            await _analiseRepository.Atualizar(analise);

            return Ok(analise);
        }


        [HttpDelete("excluir/{id:guid}")]
        public async Task<IActionResult> Excluir([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("O ID da análise deve ser informado.");

            var analise = await _analiseRepositoryRead.ObterId(id);

            if (analise == null)
                return NotFound($"Nenhuma análise encontrada com o ID {id}.");

            await _analiseRepository.Deletar(analise);

            return NoContent(); // Retorna 204 No Content após exclusão
        }


    }
}
