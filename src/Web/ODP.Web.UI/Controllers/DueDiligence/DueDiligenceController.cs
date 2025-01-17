using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ODP.Web.UI.Models.DueDiligence;
using ODP.Web.UI.Services.DueDiligence;
using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ODP.Web.UI.Controllers.DueDiligence
{
    public class DueDiligenceController : MainController
    {
        private readonly IDueService _dueService;

        public DueDiligenceController(IDueService dueService)
        {
            _dueService = dueService;
        }

        [HttpGet]

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string orgao = null, string cpf = null)
        {


            if (!cpf.IsNullOrEmpty())
            {
                cpf = FormatarCPF(cpf);
            }

            var due = await _dueService.Listar(pageNumber, pageSize, orgao,cpf);

            ViewBag.CPFAtual = cpf;
            ViewBag.OrgaoAtual = orgao;
            return View(due);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var due = await _dueService.ObterId(id);

            return View(due);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DueDiligenceViewModel dueViewModel)
        {



            await _dueService.Adicionar(CalculoRisco(dueViewModel));
            return RedirectToAction(nameof(Index));


        }


        public async Task<IActionResult> Editar(Guid id)
        {
            var due = await _dueService.ObterId(id);
            return View(due);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, DueDiligenceViewModel dueViewModel)
        {
            if (id != dueViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _dueService.Alterar(CalculoRisco(dueViewModel), id);
                bool edicaoConcluida = true;
                return RedirectToAction(nameof(Index), new { edicaoConcluida = edicaoConcluida });
            }
            return View(dueViewModel);
        }

        public async Task<IActionResult> Deletar(Guid id)
        {
            await _dueService.Deletar(id);
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Duplicar(Guid id)
        {
            var response = await _dueService.ObterId(id);
            return View(response);
        }

        [HttpPost]

        public async Task<IActionResult> Duplicar(DueDiligenceViewModel due)
        {
            if (ModelState.IsValid)
            {
                due.Id = new Guid();
                await Create(due);
                bool duplicataConcluida = true;
                return RedirectToAction(nameof(Index), new { duplicataConcluida = duplicataConcluida });
            }
            return View(due);
        }
        public async Task<IActionResult> GerarPdf(Guid id)
        {
            var file = await _dueService.GerarPdf(id);
            return file;
        }

        public DueDiligenceViewModel CalculoRisco(DueDiligenceViewModel due)
        {
            var risco = 0.0; // Convertido para double
            var valorMax = 320.0; // Convertido para double

            // Cálculo do risco
            risco += due.CadEmpresario ? 5 : 0;
            risco += due.CadEmpresaContrato ? 10 : 0;
            risco += due.CadEmpresaInidoneos ? 30 : 0;
            risco += due.CadInelegivel ? 30 : 0;
            risco += due.CadExpulsoFederal ? 10 : 0;
            risco += due.CadMEI ? 10 : 0;
            risco += due.CadPEP ? 5 : 0;
            risco += due.CadPPE ? 10 : 0; //aqui
            risco += due.CadVinculoPEP ? 5 : 0;
            risco += due.CadDoacaoEleitoralCandidato ? 10 : 0;
            risco += due.CadDoacaoEleitoralPartido ? 10 : 0;
            risco += due.CadFornecedorPartido ? 10 : 0;
            risco += due.CadFornecedorCandidato ? 10 : 0;
            risco += due.CadFiliacaoPartido ? 5 : 0;
            risco += due.CadEmpresaInidoneos ? 10 : 0;
            risco += due.CadFuncionarioPublico ? 10 : 0;
            risco += due.CadAposentado ? 10 : 0;
            risco += due.CadTrabalhoEscravo ? 50 : 0;
            risco += due.CadPrisao ? 20 : 0;
            risco += due.CadOFAC ? 10 : 0;
            risco += due.CadInabilitado ? 10 : 0;
            risco += due.CadInidoneo ? 20 : 0;
            risco += due.CadRestricoesTCE ? 10 : 0;
            risco += due.CadInadimplentesTCE ? 10 : 0;
            risco += due.CadIrregularidadesTCE ? 10 : 0;

            // Normalização do risco para o intervalo de 0 a 10
            var riscoNormalizado = (risco / valorMax) * 10;

            // Atribuição do valor normalizado à propriedade ClassificacaoRisco
            due.ClassificacaoRisco = (int)riscoNormalizado;

            return due;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf)
        {
            var due = await _dueService.BuscarPorCPF(cpf);
            return View(due);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAndGeneratePdf(DueDiligenceViewModel dueViewModel)
        {
            // Salvar o Due Diligence
             var result  = await _dueService.Adicionar(CalculoRisco(dueViewModel));

            // Gerar o PDF e retornar o resultado para download
            var pdfFile = await _dueService.GerarPdf(result.Id);

            return pdfFile;
        }

        public static string FormatarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                throw new ArgumentException("CPF inválido. Deve conter exatamente 11 dígitos numéricos.");
            }

            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
        }

    }

}
