using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Compras;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Compras
{
    public class LicitacaoController : MainController
    {
        private readonly ILicitacaoService _licitacaoService;

        public LicitacaoController(ILicitacaoService licitacaoService)
        {
            _licitacaoService = licitacaoService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _licitacaoService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var licitacao = await _licitacaoService.ObterId(id);

            return View(licitacao);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var licitacao = await _licitacaoService.BuscarCNPJ(cnpj);
            return View(licitacao);
        }




    }
}
