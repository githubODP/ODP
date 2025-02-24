using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Cooperacao;
using ODP.Web.UI.Services.Cooperacao;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Create([FromForm] TermoCooperacaoViewModel termo)
        {
            if (termo == null)
                return BadRequest("O termo de cooperação não pode ser nulo.");

            var resultado = await _cooperacaoService.Adicionar(termo);
            if (resultado == null)
                return BadRequest("Erro ao adicionar o termo de cooperação.");

            return RedirectToAction("Index"); // Redireciona para a lista após adicionar
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
        public async Task<IActionResult> Editar(Guid id, [FromForm] TermoCooperacaoViewModel termo)
        {
            if (termo == null)
                return BadRequest("O termo de cooperação não pode ser nulo.");

            var resultado = await _cooperacaoService.Alterar(id, termo);
            if (resultado == null)
                return BadRequest("Erro ao atualizar o termo de cooperação.");

            return RedirectToAction("Index");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var resultado = await _cooperacaoService.Deletar(id);

            if (resultado == null)
            {
                return BadRequest("Erro ao deletar o termo de cooperação.");
            }

            return RedirectToAction("Index");
        }





    }
}



