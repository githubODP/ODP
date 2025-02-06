using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Cooperacao;
using ODP.Web.UI.Services.Cooperacao;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Cooperacao
{
    public class CooperacaoController : Controller
    {
        private readonly IcooperacaoServices _cooperacaoServices;

        public CooperacaoController(IcooperacaoServices cooperacaoServices)
        {
            _cooperacaoServices = cooperacaoServices;
        }



        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var resultado = await _cooperacaoServices.ListarComFiltros(pageNumber, pageSize);

            if (resultado == null || !resultado.Results.Any())
                return NotFound("Nenhuma análise encontrada.");

            return View(resultado);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Create(TermoCooperacaoViewModel termo)
        {
            if (!ModelState.IsValid)
            {
                return View(termo);

            }
            var resultado = await _cooperacaoServices.Adicionar(termo);
            return View(resultado);

        }


        [HttpDelete]

        public async Task<IActionResult> Excluir (TermoCooperacaoViewModel termo)
        {
            if (!ModelState.IsValid)
            {
                return View(termo);

            }
            var resultado = await _cooperacaoServices.Deletar(termo);
            return RedirectToAction("Index");
        }



        [HttpGet]

        public IActionResult Editar(string id)
        {

            var termo = _cooperacaoServices.ObterId(id);
            return View(termo);
        }

        [HttpPost]

        public async Task<IActionResult> Editar(TermoCooperacaoViewModel termo)
        {
            if (!ModelState.IsValid)
            {
                return View(termo);

            }
            var resultado = await _cooperacaoServices.Alterar(termo);
            return RedirectToAction("Index");
        }

    }
}
