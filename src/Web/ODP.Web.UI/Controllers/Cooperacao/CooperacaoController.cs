using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Cooperacao;
using ODP.Web.UI.Services.Cooperacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Cooperacao
{

    public class CooperacaoController : Controller
    {
        private readonly ICooperacaoService _cooperacaoService;

        public CooperacaoController(ICooperacaoService cooperacaoService)
        {
            _cooperacaoService = cooperacaoService;
        }



        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string termo = null)
        {           
            var cooperacaoList = await _cooperacaoService.Listar(pageNumber, pageSize, termo);            
            var alertas = await _cooperacaoService.VerificarAlertasFimVigencia();
          
            ViewBag.TermoAtual = termo; 
            ViewBag.alertas = alertas ?? new List<TermoCooperacaoViewModel>(); 
            return View(cooperacaoList);
        }


        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var termo = await _cooperacaoService.ObterId(id);

            return View(termo);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(TermoCooperacaoViewModel termo)
        {
            if (ModelState.IsValid)
            {
                await _cooperacaoService.Adicionar(termo);
                return RedirectToAction(nameof(Index));
            }
            return View(termo);
        }


        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var termo = await _cooperacaoService.ObterId(id);
            if (termo == null)
            {
                return NotFound();
            }
            return View(termo);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, TermoCooperacaoViewModel termo)
        {
            if (id != termo.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // Log dos erros de validação
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(termo);
            }

            try
            {
                await _cooperacaoService.Alterar(id, termo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log da exceção
                Console.WriteLine($"Erro ao atualizar o termo: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar o termo.");
                return View(termo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var termo = await _cooperacaoService.ObterId(id);
            if (termo == null)
            {
                return NotFound();
            }
            return View(termo);
        }

        
        [HttpPost, ActionName("Excluir")]
        public async Task<IActionResult> ConfirmarExclusao(Guid id)
        {
            await _cooperacaoService.Deletar(id);
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> AlertasFimVigencia()
        {
            var termos = await _cooperacaoService.VerificarAlertasFimVigencia();
            return View(termos);
        }

        





    }
}



