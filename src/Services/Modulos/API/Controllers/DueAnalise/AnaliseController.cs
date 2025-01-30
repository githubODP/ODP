using Domain.DueDiligence.Entidade;
using Domain.DueDiligence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

            var resultados = await _dueRepositoryRead.ObterPorProtocolo(nroProtocolo);

            if (resultados == null || !resultados.Any())
                return NotFound(new { sucesso = false, mensagem = $"Nenhum comissionado encontrado com o número de protocolo {nroProtocolo}." });

            return Ok(new
            {
                sucesso = true,
                dados = resultados.Select(r => new
                {
                    r.NroProtocolo,
                    r.Nome,
                    r.CPF,
                    r.Orgao,
                    r.Observacao,
                    r.Id
                })
            });
        }



        [HttpPost("adicionar")]
        public async Task<IActionResult> Criar([FromBody] AnaliseCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Os dados da análise são obrigatórios.");

            if (string.IsNullOrEmpty(dto.NroProtocolo))
                return BadRequest("O número do protocolo deve ser informado.");

            // Buscar o ComissionadoId automaticamente pelo NroProtocolo
            var comissionados = await _dueRepositoryRead.ObterPorProtocolo(dto.NroProtocolo);

            if (comissionados == null || !comissionados.Any())
                return NotFound($"Nenhum comissionado encontrado para o protocolo {dto.NroProtocolo}.");

            // Escolher qual ComissionadoId usar (se houver mais de um, pode ser ajustado)
            var comissionadoId = comissionados.First().Id;

            // Obter e-mail do usuário logado
            var emailUsuarioLogado = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (string.IsNullOrEmpty(emailUsuarioLogado))
                return Unauthorized("O usuário logado não possui um e-mail válido.");

            // Criar uma nova análise vinculando ao ComissionadoId correto
            var analise = new Analise
            {
                NroProtocolo = dto.NroProtocolo,
                DataAnalise = DateTime.Now,
                AnaliseTecnica = dto.AnaliseTecnica,
                Risco = dto.Risco,
                Ressalvas = dto.Ressalvas,
                Responsavel = emailUsuarioLogado,
                ComissionadoId = comissionadoId // 🔴 Vinculando o ID correto
            };

            await _analiseRepository.Adicionar(analise);

            return CreatedAtAction(nameof(ObterPorId), new { id = analise.Id }, new
            {
                analise.Id,
                analise.NroProtocolo,
                analise.DataAnalise,
                analise.AnaliseTecnica,
                analise.Risco,
                analise.Ressalvas,
                analise.Responsavel,
                analise.ComissionadoId
            });
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
               .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            if (string.IsNullOrWhiteSpace(emailUsuarioLogado))
                return Unauthorized("O usuário logado não possui um e-mail válido.");

            // Buscar o comissionado correspondente (se necessário)
            var comissionado = await _dueRepositoryRead.ObterId(analise.ComissionadoId);
            if (comissionado == null)
                return NotFound("O comissionado vinculado a esta análise não foi encontrado.");

            // Atualizar os dados da análise
            analise.NroProtocolo = comissionado.NroProtocolo;  // Mantendo o vínculo correto
            analise.DataAnalise = DateTime.Now;
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

            try
            {
                await _analiseRepository.Deletar(analise);
                return Ok(new { sucesso = true, mensagem = "Análise excluída com sucesso." });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new { sucesso = false, mensagem = "Erro ao excluir a análise. Verifique se há dependências.", erro = ex.Message });
            }
        }



    }
}
