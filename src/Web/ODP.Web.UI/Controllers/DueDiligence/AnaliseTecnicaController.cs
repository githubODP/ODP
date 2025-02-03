using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.DueDiligence;
using ODP.Web.UI.Services.DueDiligence;
using System;
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

            if (resultado == null || !resultado.Results.Any())
                return NotFound("Nenhuma análise encontrada.");

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
        public IActionResult Create()
        {
            var model = new AnaliseCadastroViewModel
            {
                DataAnalise = DateTime.Now // Define a data da análise automaticamente
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




        //[HttpPost]
        //public async Task<IActionResult> Editar(Guid id, AnaliseCadastroModel analiseViewModel)
        //{
        //    if (id != analiseViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        await _analiseService.Alterar(analiseViewModel, id);
        //        bool edicaoConcluida = true;
        //        return RedirectToAction(nameof(Index), new { edicaoConcluida = edicaoConcluida });
        //    }
        //    return View(analiseViewModel);
        //}



        //[HttpDelete]
        //public async Task<IActionResult> Deletar(Guid id)
        //{

        //    await _analiseService.Deletar(id);
        //    return RedirectToAction(nameof(Index));


        //}

    }
}
