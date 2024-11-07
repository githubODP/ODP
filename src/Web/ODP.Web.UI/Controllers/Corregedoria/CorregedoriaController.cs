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
            var instauracao = await _instauracaoService.Listar(pageNumber, pageSize);
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

        //[HttpGet]
        //public IActionResult Criar()
        //{
        //    return View();
        //}

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
            var paginatedResult = await _instauracaoService.UploadCsv(file, 10, 1);

            return View("Confirmacao", paginatedResult);
        }

    }

}
