using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.DueDiligence;
using ODP.Web.UI.Services.DueDiligence;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string nome = null, string cpf = "123", string protocolo = null)
        {


            if (cpf != "123") { cpf = FormatarCPF(cpf); }
            else { cpf = null; }

            var due = await _dueService.Listar(pageNumber, pageSize, nome, cpf, protocolo);

            ViewBag.CPFAtual = cpf;
            ViewBag.NomeAtual = nome;
            ViewBag.ProtocoloAtual = protocolo;
            return View(due);
        }

        [HttpGet]  /// lista due do gabinete

        public async Task<IActionResult> ListarDue(int pageNumber = 1, int pageSize = 5, string nome = null, string cpf = "123", string protocolo = null)
        {


            if (cpf != "123") { cpf = FormatarCPF(cpf); }
            else { cpf = null; }

            var ListarDue = await _dueService.Listar(pageNumber, pageSize, nome, cpf, protocolo);

            ViewBag.CPFAtual = cpf;
            ViewBag.NomeAtual = nome;
            ViewBag.ProtocoloAtual = protocolo;
            return View("ListarDue", ListarDue);

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
            var result = await _dueService.Adicionar(CalculoRisco(dueViewModel));

            // Gerar o PDF e retornar o resultado para download
            var pdfFile = await _dueService.GerarPdf(result.Id);

            return pdfFile;
        }

        public static string FormatarCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF não pode ser nulo ou vazio.");

            // Remove qualquer caractere que não seja número
            string numerosCPF = new string(cpf.Where(char.IsDigit).ToArray());

            // Verifica se o CPF tem exatamente 11 números
            if (numerosCPF.Length != 11)
                throw new ArgumentException("CPF inválido. Deve conter exatamente 11 dígitos numéricos.");

            // Validação do CPF pelo cálculo dos dígitos verificadores
            if (!ValidarCPF(numerosCPF))
                throw new ArgumentException("CPF inválido.");

            // Retorna o CPF formatado
            return $"{numerosCPF.Substring(0, 3)}.{numerosCPF.Substring(3, 3)}.{numerosCPF.Substring(6, 3)}-{numerosCPF.Substring(9, 2)}";
        }

        private static bool ValidarCPF(string cpf)
        {
            // Verifica se todos os dígitos são iguais (ex: 000.000.000-00), o que é inválido
            if (cpf.Distinct().Count() == 1)
                return false;

            // Calcula os dois dígitos verificadores
            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = tempCpf.Select((t, i) => (t - '0') * multiplicadores1[i]).Sum();
            int resto = (soma * 10) % 11;
            if (resto == 10) resto = 0;
            if (resto != (cpf[9] - '0')) return false;

            tempCpf += resto;
            soma = tempCpf.Select((t, i) => (t - '0') * multiplicadores2[i]).Sum();
            resto = (soma * 10) % 11;
            if (resto == 10) resto = 0;

            return resto == (cpf[10] - '0');
        }

    }

}
