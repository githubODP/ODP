using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.DueDiligence;
using ODP.Web.UI.Services.DueDiligence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.DueDiligence
{
    public class AnaliseTecnicaController : Controller
    {
        private readonly IAnaliseService _analiseService;

        public AnaliseTecnicaController(IAnaliseService analiseService)
        {
            _analiseService = analiseService;
        }




        [HttpGet]
        public async Task<IActionResult> ListarDadosAdicionais([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var resultado = await _analiseService.ListarDadosAdicionais(pageNumber, pageSize);

            if (resultado == null || resultado.Results == null || !resultado.Results.Any())
            {
                ViewBag.Mensagem = "Nenhuma análise encontrada.";
                resultado = new PagedResult<AnaliseViewModel> // Criar um objeto vazio para evitar erro na View
                {
                    Results = new List<AnaliseViewModel>(),
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = 0,
                    TotalRecords = 0
                };
            }

            return View(resultado);
        }


        [HttpGet]
        public IActionResult PesquisarProtocolo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PesquisarProtocolo(string nroProtocolo)
        {
            if (string.IsNullOrWhiteSpace(nroProtocolo))
            {
                ViewData["Mensagem"] = "Número do protocolo deve ser informado.";
                return View();
            }

            var resultado = await _analiseService.PesquisarComissionado(nroProtocolo);

            if (resultado == null || resultado.Count == 0)
            {
                ViewData["Mensagem"] = "Nenhum comissionado encontrado.";
                return View();
            }

            return View(resultado);
        }




        [HttpGet]
        public IActionResult Create(string nroProtocolo)
        {
            var emailUsuario = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var model = new AnaliseCadastroViewModel
            {
                DataAnalise = DateTime.Now, // Define a data da análise automaticamente
                Responsavel = emailUsuario,
                NroProtocolo = nroProtocolo,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnaliseCadastroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }

            // Obtém o e-mail do usuário logado
            var emailUsuario = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!string.IsNullOrWhiteSpace(emailUsuario))
            {
                model.Responsavel = emailUsuario;
            }


            model.DataAnalise = DateTime.Now;

            var resultado = await _analiseService.Adicionar(model);

            if (resultado == null)
            {
                ModelState.AddModelError("", "Erro ao salvar a análise.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Análise criada com sucesso!";

            // Redireciona para a listagem correta após a criação
            return RedirectToAction("ListarDadosAdicionais");

        }



        [HttpGet("Editar/{id}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var analise = await _analiseService.ObterId(id);
            if (analise == null)
            {
                TempData["ErrorMessage"] = "Análise não encontrada.";
                return RedirectToAction("ListarDadosAdicionais");
            }


            return View(analise);
        }

        [HttpPost("Editar/{id}")]
        public async Task<IActionResult> Editar(Guid id, AnaliseCadastroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await _analiseService.Alterar(id, model);
            if (resultado == null)
            {
                ModelState.AddModelError("", "Erro ao atualizar a análise.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Análise atualizada com sucesso!";
            return RedirectToAction("ListarDadosAdicionais");
        }



        [HttpGet]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var analise = await _analiseService.ObterId(id);
            if (analise == null)
            {
                TempData["ErrorMessage"] = "Análise não encontrada.";
                return RedirectToAction("ListarDadosAdicionais");
            }

            return View(analise);
        }


        [HttpPost, ActionName("Excluir")]
        public async Task<IActionResult> ConfirmarExclusao(Guid id)
        {
            await _analiseService.Deletar(id);
            return RedirectToAction(nameof(ListarDadosAdicionais));
        }


        //[HttpPost, ActionName("Excluir")]
        //public async Task<IActionResult> DeletarConfirmado(Guid id)
        //{
        //    var resultado = await _analiseService.Deletar(id);
        //    if (resultado == null)
        //    {
        //        TempData["ErrorMessage"] = "Erro ao excluir a análise.";
        //        return RedirectToAction("ListarDadosAdicionais");
        //    }

        //    TempData["SuccessMessage"] = "Análise excluída com sucesso!";
        //    return RedirectToAction("ListarDadosAdicionais");
        //}


    }
}
