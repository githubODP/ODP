using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Internos;
using ODP.Web.UI.Services.Internos;
using System.Threading.Tasks;
using System;

namespace ODP.Web.UI.Controllers.Internos
{
    public class DemandasController : Controller
    {
        private readonly IDemandaService _Service;

        public DemandasController (IDemandaService sevice)
        {
            _Service = sevice;
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

        public async Task<IActionResult> Create(DemandasViewModel demandas)
        {
            if (!ModelState.IsValid)
            {
                return View(demandas);

            }
            var resultado = await _Service.Adicionar(demandas);
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

        public async Task<IActionResult> Editar(DemandasViewModel demandas)
        {
            if (!ModelState.IsValid)
            {
                return View(demandas);

            }

            var resultado = await _Service.Alterar(demandas);
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
