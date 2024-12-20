﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ODP.Web.UI.Models.Corregedoria;
using ODP.Web.UI.Services.Corregedoria;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Corregedoria
{
    public class CorregedoriaController : MainController
    {
        private readonly IInstauracaoService _instauracaoService;
        private readonly ILogger<CorregedoriaController> _logger;

        public CorregedoriaController(IInstauracaoService instauracaoService, ILogger<CorregedoriaController> logger )
        {
            _instauracaoService = instauracaoService;
            _logger = logger;
        }




        [HttpGet]
        public async Task<IActionResult> Index(
            int pageNumber = 1,
            int pageSize = 5,
            int? ano = null,
            string orgao = null,
            string procedimento = null,
            string decisao = null,
            string protocolo = null) // Adiciona o parâmetro protocolo
        {
            // Chama o serviço com os filtros ou protocolo
            var instauracao = await _instauracaoService.ListarComFiltros(
                pageNumber,
                pageSize,
                ano,
                orgao,
                procedimento,
                decisao,
                protocolo); // Inclui protocolo na chamada

            // Armazena os valores atuais dos filtros para reutilizar na view
            ViewBag.AnoAtual = ano;
            ViewBag.OrgaoAtual = orgao;
            ViewBag.ProcedimentoAtual = procedimento;
            ViewBag.DecisaoAtual = decisao;
            ViewBag.ProtocoloAtual = protocolo; // Armazena o protocolo na ViewBag

            return View(instauracao);
        }





        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            // Chama o serviço para obter o registro pelo ID
            var instauracao = await _instauracaoService.ObterId(id);

            // Verifica se o registro foi encontrado
            if (instauracao == null)
            {
                // Retorna um erro 404 caso não encontre o registro
                return NotFound("Registro não encontrado.");
            }

            // Retorna a View com o objeto encontrado
            return View(instauracao);
        }




        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InstauracaoViewModel instauracaoViewModel)
        {
            if (!ModelState.IsValid)
                return View(instauracaoViewModel);

            // Valida o tipo de decisão
            var tiposPermitidos = new[] { "TAC_CELEBRADO", "TAC_CONCLUIDO", "TAC_DESCUMPRIDO", "TAC_INVIAVEL" };
            if (tiposPermitidos.Contains(instauracaoViewModel.Decisao.ToString()))
            {
                // Calcula o prazo de encerramento
                if (instauracaoViewModel.DataInicioTac.HasValue && instauracaoViewModel.DataFimTac.HasValue)
                {
                    instauracaoViewModel.PrazoEncerra =
                        (instauracaoViewModel.DataFimTac.Value - instauracaoViewModel.DataInicioTac.Value).Days;
                }
            }
            else
            {
                // Zera os campos TAC
                instauracaoViewModel.DataInicioTac = null;
                instauracaoViewModel.DataFimTac = null;
                instauracaoViewModel.PrazoEncerra = null;
                instauracaoViewModel.PGE = null;
                instauracaoViewModel.Cumpriu = null;
                instauracaoViewModel.ObservacaoAjusteTAC = null;
            }

            await _instauracaoService.Adicionar(instauracaoViewModel);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var instauracao = await _instauracaoService.ObterId(id);
            if (instauracao == null)
            {
                return NotFound("Registro não encontrado.");
            }
            return View(instauracao);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, InstauracaoViewModel instauracaoViewModel)
        {
            try
            {
                // Valida se o ID fornecido corresponde ao da ViewModel
                if (id != instauracaoViewModel.Id)
                {
                    return BadRequest("O ID fornecido não corresponde ao registro.");
                }

                // Valida o estado do modelo
                if (!ModelState.IsValid)
                {
                    return View(instauracaoViewModel);
                }

                // Verifica e limpa os campos TAC, se necessário
                ResetarCamposTacSeNaoSelecionado(instauracaoViewModel);

                // Log dos dados recebidos
                _logger.LogInformation($"Iniciando alteração para ID: {id}");
                _logger.LogInformation($"Decisao: {instauracaoViewModel.Decisao}");

                // Chama o serviço para atualizar os dados
                await _instauracaoService.Alterar(instauracaoViewModel, id);

                _logger.LogInformation("Alteração concluída com sucesso.");

                // Redireciona após edição bem-sucedida
                return RedirectToAction(nameof(Index), new { edicaoConcluida = true });
            }
            catch (Exception ex)
            {
                // Log do erro
                _logger.LogError($"Erro ao editar a instauração. ID: {id}. Erro: {ex.Message}");

                // Retorna status 500 com mensagem amigável
                return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
            }
        }

        /// <summary>
        /// Reseta os campos TAC caso a decisão não seja uma das opções válidas.
        /// </summary>
        private void ResetarCamposTacSeNaoSelecionado(InstauracaoViewModel instauracaoViewModel)
        {
            var tacDecisions = new[] { "TAC_CELEBRADO", "TAC_CONCLUIDO", "TAC_DESCUMPRIDO", "TAC_INVIAVEL" };

            if (!tacDecisions.Contains(instauracaoViewModel.Decisao.ToString()))
            {
                instauracaoViewModel.PGE = null;
                instauracaoViewModel.Cumpriu = null;
                instauracaoViewModel.DataInicioTac = null;
                instauracaoViewModel.DataFimTac = null;
                instauracaoViewModel.PrazoEncerra = null;
                instauracaoViewModel.ObservacaoAjusteTAC = null;
            }
        }




        [HttpDelete]
        public async Task<IActionResult> Deletar(Guid id)
        {

            var existente = await _instauracaoService.ObterId(id);
            if (existente == null)
            {
                return NotFound("Registro não encontrado.");
            }

            await _instauracaoService.Deletar(id);
            return RedirectToAction(nameof(Index));


        }
        //public async Task<IActionResult> GerarPdf(Guid id)
        //{
        //    var file = await _instauracaoService.GerarPdf(id);
        //    return file;
        //}

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["MensagemErro"] = "Por favor, selecione um arquivo válido.";
                return RedirectToAction(nameof(Upload));
            }

            try
            {
                bool sucesso = await _instauracaoService.UploadCsv(file);

                if (sucesso)
                {
                    TempData["MensagemSucesso"] = "Arquivo processado com sucesso!";
                    return RedirectToAction(nameof(Index)); // Redireciona para a página principal (Index)
                }
                else
                {
                    TempData["MensagemErro"] = "Falha ao processar o arquivo.";
                    return RedirectToAction(nameof(Upload)); // Retorna para a página de upload
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro: {ex.Message}";
                return RedirectToAction(nameof(Upload)); // Retorna para a página de upload em caso de exceção
            }
        }


        [Authorize]
        [HttpGet]
        public IActionResult Graficos()
        {
            return View();
        }

    }

}
