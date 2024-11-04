using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.DueDiligence;
using ODP.Web.UI.Services.Demanda;
using System.Threading.Tasks;
using System;
using ODP.Web.UI.Models.Demandas;
using ODP.Web.UI.Models.Corregedoria;

namespace ODP.Web.UI.Controllers.Demanda
{
    public class DemandaController : MainController
    {
        private readonly IDemandaService _demandaService;
        
        public DemandaController (IDemandaService demandaService)
        {
            _demandaService = demandaService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var due = await _demandaService.Listar(pageNumber, pageSize);
            return View(due);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var due = await _demandaService.ObterId(id);

            return View(due);
        }
        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DemandaViewModel demandaViewModel)
        {

            if (ModelState.IsValid)
            {
                await _demandaService.Adicionar(demandaViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(demandaViewModel);

        }

        public async Task<IActionResult> Editar(Guid id)
        {
            var demanda = await _demandaService.ObterId(id);
            return View(demanda);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, DemandaViewModel demandaViewModel)
        {
            if (id != demandaViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _demandaService.Alterar(demandaViewModel, id);
                bool edicaoConcluida = true;
                return RedirectToAction(nameof(Index), new { edicaoConcluida = edicaoConcluida });
            }
            return View(demandaViewModel);
        }



        [HttpDelete]
        public async Task<IActionResult> Deletar(Guid id)
        {

            await _demandaService.Deletar(id);
            return RedirectToAction(nameof(Index));


        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var demanda = await _demandaService.BuscarCPF(cnpj);
            return View(demanda);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var demanda = await _demandaService.BuscarCPF(cpf);
            return View(demanda);
        }

    }
}
