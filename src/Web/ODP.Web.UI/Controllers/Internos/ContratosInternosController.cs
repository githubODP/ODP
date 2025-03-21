﻿using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Internos;
using ODP.Web.UI.Services.Internos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Internos
{
    public class ContratosInternosController : Controller
    {
        private readonly IContratosInternosService _contratoInternosService;
        
        public ContratosInternosController (IContratosInternosService contratoInternoservice)
        {
            _contratoInternosService = contratoInternoservice;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string termo = null)
        {
            var contratoList = await _contratoInternosService.Listar(pageNumber, pageSize, termo);
           

            ViewBag.TermoAtual = termo;            
            return View(contratoList);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var termo = await _contratoInternosService.ObterId(id);

            return View(termo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContratosInternosViewModel termo)
        {
            if (ModelState.IsValid)
            {
                await _contratoInternosService.Adicionar(termo);
                return RedirectToAction(nameof(Index));
            }
            return View(termo);
        }



        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var contrato = await _contratoInternosService.ObterId(id);
            if (contrato == null)
            {
                return NotFound();
            }
            return View(contrato);
        }


        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, ContratosInternosViewModel termo)
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
                await _contratoInternosService.Alterar(id, termo);
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

        [HttpPost, ActionName("Excluir")]
        public async Task<IActionResult> ConfirmarExclusao(Guid id)
        {
            await _contratoInternosService.Deletar(id);
            return RedirectToAction(nameof(Index));
        }


    }


}
