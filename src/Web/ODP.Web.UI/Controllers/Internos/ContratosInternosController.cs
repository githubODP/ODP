using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Cooperacao;
using ODP.Web.UI.Models.Internos;
using ODP.Web.UI.Services.Cooperacao;
using ODP.Web.UI.Services.Internos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Internos
{
    public class ContratosInternosController : Controller
    {
        private readonly IContratosInternosService _Service;
        
        public ContratosInternosController (IContratosInternosService service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var resultado = await _Service.Listar(pageNumber, pageSize);
            return View(resultado);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Create(ContratosInternosViewModel contratos)
        {
            if (!ModelState.IsValid)
            {
                return View(contratos);

            }
            var resultado = await _Service.Adicionar(contratos);
            return RedirectToAction("Index");


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var termo = await _Service.ObterId(id);
            var resultado = await _Service.Deletar(termo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var termo = await _Service.ObterId(Id);
            return View(termo);
        }


        [HttpPost]

        public async Task<IActionResult> Editar(ContratosInternosViewModel contratos)
        {
            if (!ModelState.IsValid)
            {
                return View(contratos);

            }
            
            var resultado = await _Service.Alterar(contratos);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var termo = await _Service.ObterId(id);

            return View(termo);
        }


    }


}
