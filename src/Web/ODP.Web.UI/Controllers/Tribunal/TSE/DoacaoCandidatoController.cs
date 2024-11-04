using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Tribunal.TSE;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Tribunal.TSE
{
    public class DoacaoCandidatoController : MainController
    {
        private readonly IDoacaoCandidatoService _doacaoService;

        public DoacaoCandidatoController(IDoacaoCandidatoService doacaoService)
        {
            _doacaoService = doacaoService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _doacaoService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var doacao = await _doacaoService.ObterId(id);

            return View(doacao);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var doacao = await _doacaoService.BuscarCNPJ(cnpj);
            return View(doacao);
        }

    }
}
