using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Internos;
using ODP.Web.UI.Services.Internos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Internos
{
    public class DemandaController : MainController
    {
        private readonly IDemandaService _demandaService;

        public DemandaController(IDemandaService demandaService)
        {
            _demandaService = demandaService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string termo = null)
        {
            var demandaList = await _demandaService.Listar(pageNumber, pageSize, termo);
            ViewBag.TermoAtual = termo;           
            return View(demandaList);
        }


        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var termo = await _demandaService.ObterId(id);

            return View(termo);
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

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var termo = await _demandaService.ObterId(id);
            if (termo == null)
            {
                return NotFound();
            }
            return View(termo);
        }


        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, DemandaViewModel demandaViewModel)
        {
            if (id != demandaViewModel.Id)
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
                return View(demandaViewModel);
            }

            try
            {
                await _demandaService.Alterar(id, demandaViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log da exceção
                Console.WriteLine($"Erro ao atualizar o Demanda: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar o termo.");
                return View(demandaViewModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var termo = await _demandaService.ObterId(id);
            if (termo == null)
            {
                return NotFound();
            }
            return View(termo);
        }


        [HttpPost, ActionName("Excluir")]
        public async Task<IActionResult> ConfirmarExclusao(Guid id)
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
