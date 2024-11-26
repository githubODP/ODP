using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Corregedoria;
using ODP.Web.UI.Services.Corregedoria;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Corregedoria
{
    public class CorregedoriaController : MainController
    {
        private readonly IInstauracaoService _instauracaoService;

        public CorregedoriaController(IInstauracaoService instauracaoService)
        {
            _instauracaoService = instauracaoService;
        }


        [HttpGet]
        public IActionResult Menu()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 8)
        {
            // Obtém os dados paginados
            var instauracao = await _instauracaoService.Listar(pageNumber, pageSize);

            // Calcula o número total de páginas usando TotalRecords
            int totalPages = (int)Math.Ceiling((double)instauracao.TotalRecords / pageSize);

            // Redireciona para a última página caso o usuário esteja na primeira e existam itens
            if (pageNumber == 1 && instauracao.TotalRecords > 0)
            {
                pageNumber = totalPages;
                return RedirectToAction(nameof(Index), new { pageNumber, pageSize });
            }

            // Retorna a view com os dados
            return View(instauracao);
        }



        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var instauracao = await _instauracaoService.ObterId(id);

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


            {
                await _instauracaoService.Adicionar(instauracaoViewModel);
                return RedirectToAction(nameof(Index));
            }


        }

      
        public async Task<IActionResult> Editar(Guid id)
        {
            var instauracao = await _instauracaoService.ObterId(id);
            return View(instauracao);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, InstauracaoViewModel instauracaoViewModel)
        {
            if (id != instauracaoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _instauracaoService.Alterar(instauracaoViewModel, id);
                bool edicaoConcluida = true;
                return RedirectToAction(nameof(Index), new { edicaoConcluida = edicaoConcluida });
            }
            return View(instauracaoViewModel);
        }





        [HttpDelete]
        public async Task<IActionResult> Deletar(Guid id)
        {

            await _instauracaoService.Deletar(id);
            return RedirectToAction(nameof(Index));


        }
        public async Task<IActionResult> GerarPdf(Guid id)
        {
            var file = await _instauracaoService.GerarPdf(id);
            return file;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            try
            {
                bool sucesso = await _instauracaoService.UploadCsv(file);

                if (sucesso)
                {
                    TempData["MensagemSucesso"] = "Arquivo processado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["MensagemErro"] = "Falha ao processar o arquivo.";
                    return RedirectToAction(nameof(Upload));
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = $"Erro: {ex.Message}";
                return RedirectToAction(nameof(Upload));
            }
        }


    }

}
