using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Demandas;
using ODP.Web.UI.Models.DueDiligence;
using ODP.Web.UI.Services.DueDiligence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.DueDiligence
{
    public class AnaliseTecnicaController : Controller
    {
        private readonly IAnaliseService _analiseService;

        public AnaliseTecnicaController (IAnaliseService analiseService)
        {
            _analiseService = analiseService;
        }




        [HttpGet]
        public async Task<IActionResult> ListarDadosAdicionais([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var resultado = await _analiseService.ListarDadosAdicionais(pageNumber, pageSize);

            if (resultado == null || !resultado.Results.Any())
                return NotFound("Nenhuma análise encontrada.");

            return View(resultado); 
        }

        [HttpGet]
        public async Task<IActionResult> PesquisarProtocolo(string nroProtocolo)
        {
            if (string.IsNullOrEmpty(nroProtocolo))
                return Json(new { sucesso = false, mensagem = "Número do protocolo deve ser informado." });

            var resultado = await _analiseService.PesquisarComissionado(nroProtocolo);

            if (resultado == null)
                return Json(new { sucesso = false, mensagem = "Nenhum comissionado encontrado." });

            return Json(new { sucesso = true, dados = resultado });
        }



        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnaliseCadastroViewModel analisecadastroViewModel)
        {

            if (ModelState.IsValid)
            {
                await _analiseService.Adicionar(analisecadastroViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(analisecadastroViewModel);

        }

        public async Task<IActionResult> Editar(Guid id)
        {
            var demanda = await _analiseService.ObterId(id);
            return View(demanda);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, AnaliseCadastroViewModel analisecadastroViewModel)
        {
            if (id != analisecadastroViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _analiseService.Alterar(analisecadastroViewModel, id);
                bool edicaoConcluida = true;
                return RedirectToAction(nameof(Index), new { edicaoConcluida = edicaoConcluida });
            }
            return View(analisecadastroViewModel);
        }



        [HttpDelete]
        public async Task<IActionResult> Deletar(Guid id)
        {

            await _analiseService.Deletar(id);
            return RedirectToAction(nameof(Index));


        }

    }
}
