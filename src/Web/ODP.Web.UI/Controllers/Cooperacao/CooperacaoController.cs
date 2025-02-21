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
            var resultado = await _cooperacaoService.Adicionar(termo);
            return View(resultado);

        }


        [HttpPost] // Alterado de [HttpDelete] para [HttpPost]
        [ValidateAntiForgeryToken] // Adicione este atributo para segurança
        public async Task<IActionResult> Excluir(Guid id)
        {
            var termo = await _cooperacaoService.ObterId(id);
            var resultado = await _cooperacaoService.Deletar(termo);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var termo = await _cooperacaoService.ObterId(Id);
            return View(termo);
        }


        [HttpPost]

        public async Task<IActionResult> Editarpost(TermoCooperacaoViewModel termo)
        {
            if (!ModelState.IsValid)
            {
                return View(termo);

            }
            var alterar = await _cooperacaoService.ObterId(termo.Id);
            var resultado = await _cooperacaoService.Alterar(copiaTermo(termo, alterar));
            return RedirectToAction("Index");
        }


        private TermoCooperacaoViewModel copiaTermo(TermoCooperacaoViewModel copia, TermoCooperacaoViewModel copiado)
        {
            if (copia == null || copiado == null)
                throw new ArgumentNullException("Os objetos não podem ser nulos");

            var propriedades = typeof(TermoCooperacaoViewModel).GetProperties();

            foreach (var propriedade in propriedades)
            {
                if (!propriedade.CanRead || !propriedade.CanWrite) continue;

                var valorCopia = propriedade.GetValue(copia);
                var valorCopiado = propriedade.GetValue(copiado);

                // Se os valores forem diferentes, atualiza o valor no objeto copiado
                if ((valorCopia != null && !valorCopia.Equals(valorCopiado)) || (valorCopia == null && valorCopiado != null))
                {
                    propriedade.SetValue(copiado, valorCopia);
                }
            }

            return copiado;
        }



        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var termo = await _cooperacaoService.ObterId(id);

            return View(termo);
        }





    }
}



