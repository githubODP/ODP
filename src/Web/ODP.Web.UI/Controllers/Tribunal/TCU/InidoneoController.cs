using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCU;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Tribunal.TCU
{
    public class InidoneoController : MainController
    {
        private readonly IInidoneoService _inidoneoService;

        public InidoneoController(IInidoneoService inidoneoService)
        {
            _inidoneoService = inidoneoService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _inidoneoService.Listar(pageNumber, pageSize);
            return View(pagedResult);
        }

        [HttpGet]

        public async Task<IActionResult> Detalhes(Guid id)
        {
            var inidoneo = await _inidoneoService.ObterId(id);

            return View(inidoneo);
        }


        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj)
        {
            var inidoneo = await _inidoneoService.BuscarCNPJ(cnpj);
            return View(inidoneo);
        }

    }
}
