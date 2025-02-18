
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ODP.Web.UI.Models.Consultas.ResultadoConsulta;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ODP.Web.UI.Controllers.Consultas
{
    
    public class ConsultaController : Controller
    {
        private readonly IBuscaService _buscaService;

        public ConsultaController(IBuscaService buscaService)
        {
            _buscaService = buscaService;
        }

        // Exibe o formulário para o usuário inserir o CNPJ/CPF e carregar arquivos
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> ConsultaCNPJ(string cnpj, List<string> tabelasSelecionadas)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                ModelState.AddModelError("cnpj", "CNPJ é obrigatório.");
                return View("Index");
            }

            if (tabelasSelecionadas.Contains("todas") || !tabelasSelecionadas.Any())
            {
                tabelasSelecionadas = new List<string> { "compras", "dividas", "fazenda", "federal", "estadual", "tce", "tcu", "tse", "interno" };
            }

            // Chama o serviço de busca e obtém o resultado como um DTO com lista de contratos
            var resultado = await _buscaService.BuscarCNPJPorTabelasSelecionadas(cnpj, tabelasSelecionadas);

            // Passa o DTO com a lista de contratos para a View
            ViewBag.CNPJAtual = cnpj;
            return View("ConsultaCNPJ", resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaCPF(string cpf, List<string> tabelasSelecionadas)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                ModelState.AddModelError("cpf", "CPF é obrigatório.");
                return View("Index");
            }

            if (tabelasSelecionadas.Contains("todas") || !tabelasSelecionadas.Any())
            {
                tabelasSelecionadas = new List<string> { "compras", "dividas", "fazenda", "federal", "estadual", "tce", "tcu", "tse", "interno" };
            }

            // Chama o serviço de busca e obtém o resultado como um DTO com lista de contratos
            var resultado = await _buscaService.BuscarCPFPorTabelasSelecionadas(cpf, tabelasSelecionadas);

            // Passa o DTO com a lista de contratos para a View
            return View("ConsultaCPF", resultado);
        }



        [HttpPost]
        public async Task<IActionResult> ConsultaLote(IFormFile arquivo, string tipoDeBusca, List<string> tabelasSelecionadas)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                ModelState.AddModelError("arquivo", "Por favor, carregue um arquivo válido.");
                return View("Index");
            }

            var identificadores = new List<string>();
            using (var reader = new StreamReader(arquivo.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        identificadores.Add(line.Trim());
                    }
                }
            }

            bool isCNPJ = tipoDeBusca == "CNPJ";
            var resultados = await _buscaService.BuscarEmLoteAsync(identificadores, isCNPJ, tabelasSelecionadas);

            // Filtra apenas os resultados encontrados
            var resultadosEncontrados = resultados.Where(r =>
                (r.BuscaCompras?.Contrato?.Any() == true) ||
                (r.BuscaCompras?.Dispensa?.Any() == true) ||
                (r.BuscaCompras?.Licitacao?.Any() == true) ||
                (r.BuscaDividas?.DividaFGTS?.Any() == true) ||
                (r.BuscaDividas?.DividaPrevidenciaria?.Any() == true) ||
                (r.BuscaDividas?.DividaNaoPrevidenciaria?.Any() == true) ||
                (r.BuscaInterno?.DueDiligence?.Any() == true) ||
                (r.BuscaFazenda?.Jucepar?.Any() == true) ||
                (r.BuscaFazenda?.NFEletronica?.Any() == true) ||
                (r.BuscaFazenda?.NFEletronicaFederal?.Any() == true) ||
                (r.BuscaEstadual?.Ambiental?.Any() == true) ||
                (r.BuscaEstadual?.PADV?.Any() == true) ||
                (r.BuscaFederal?.AcordosLeniencia?.Any() == true) ||
                (r.BuscaFederal?.Aeronave?.Any() == true) ||
                (r.BuscaFederal?.BeneficioContinuo?.Any() == true) ||
                (r.BuscaFederal?.BolsaFamilia?.Any() == true) ||
                (r.BuscaFederal?.Ceis?.Any() == true) ||
                (r.BuscaFederal?.Cepim?.Any() == true) ||
                (r.BuscaFederal?.Cnep?.Any() == true) ||
                (r.BuscaFederal?.ExpulsoFederal?.Any() == true) ||
                (r.BuscaFederal?.PEP?.Any() == true) ||
                (r.BuscaFederal?.SeguroDefeso?.Any() == true) ||
                (r.BuscaFederal?.TrabalhoEscravo?.Any() == true) ||
                (r.BuscaTCE?.CNPJRestrito?.Any() == true) ||
                (r.BuscaTCE?.CPFRestrito?.Any() == true) ||
                (r.BuscaTCE?.Inadimplente?.Any() == true) ||
                (r.BuscaTCE?.Irregularidade?.Any() == true) ||
                (r.BuscaTCU?.ContaEleitoralIrregular?.Any() == true) ||
                (r.BuscaFederal?.PEP?.Any() == true) ||
                (r.BuscaTCU?.ContaIrregular?.Any() == true) ||
                (r.BuscaTCU?.Inabilitado?.Any() == true) ||
                (r.BuscaTCU?.Inidoneo?.Any() == true) ||
                (r.BuscaTSE?.DoacaoPartido?.Any() == true) ||
                (r.BuscaTSE?.DoacaoCandidato?.Any() == true) ||
                (r.BuscaTSE?.DoacaoPartidoGeral?.Any() == true) ||
                (r.BuscaTSE?.DoacaoGeral?.Any() == true) ||
                (r.BuscaTSE?.FornecedorCandidato?.Any() == true) ||
                (r.BuscaTSE?.FornecedorPartido?.Any() == true)).ToList();

            if (isCNPJ)
            {
                // Geração de Excel para CNPJ
                var arquivoExcel = GerarExcelCNPJ(resultadosEncontrados);
                return File(arquivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ResultadosCNPJ.xlsx");
            }
            else
            {
                // Geração de Excel para CPF (você pode adicionar os campos relevantes para CPF)
                var arquivoExcel = GerarExcelCPF(resultadosEncontrados);
                return File(arquivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ResultadosCPF.xlsx");
            }
        }

        private byte[] GerarExcelCNPJ(List<ResultadoDTO> resultados)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Resultados CNPJ");

                int row = 1;

                // Adicionando a seção de Contratos
                if (resultados.Any(r => r.BuscaCompras?.Contrato != null && r.BuscaCompras.Contrato.Any()))
                {
                    worksheet.Cells[row, 1].Value = "CONTRATOS";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Contratos
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Orgão Gestor";
                    worksheet.Cells[row, 3].Value = "Status";
                    worksheet.Cells[row, 4].Value = "Protocolo";
                    worksheet.Cells[row, 7].Value = "Fornecedor";
                    worksheet.Cells[row, 8].Value = "CNPJ";
                    worksheet.Cells[row, 5].Value = "Valor Total Atual";
                    worksheet.Cells[row, 6].Value = "Valor Total Original";
                    worksheet.Cells[row, 9].Value = "Data Início Vigência";
                    worksheet.Cells[row, 10].Value = "Data Fim Vigência";
                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaCompras?.Contrato != null && resultado.BuscaCompras.Contrato.Any())
                        {
                            foreach (var contrato in resultado.BuscaCompras.Contrato)
                            {
                                worksheet.Cells[row, 1].Value = "Contrato";
                                worksheet.Cells[row, 2].Value = contrato.OrgaoGestor;
                                worksheet.Cells[row, 3].Value = contrato.StatusContrato;
                                worksheet.Cells[row, 4].Value = contrato.Protocolo;
                                worksheet.Cells[row, 7].Value = contrato.Fornecedor;
                                worksheet.Cells[row, 8].Value = contrato.CNPJ;
                                worksheet.Cells[row, 5].Value = contrato.VlrTotalAtual;
                                worksheet.Cells[row, 6].Value = contrato.VlrTotalOriginal;
                                worksheet.Cells[row, 9].Value = contrato.DTInicioVigencia?.ToString("dd/MM/yyyy") ?? "N/A";
                                worksheet.Cells[row, 10].Value = contrato.DTFimVigencia?.ToString("dd/MM/yyyy") ?? "N/A";

                                row++;
                            }
                        }
                    }
                }


                // Adicionando a seção de Dispensas
                if (resultados.Any(r => r.BuscaCompras?.Dispensa != null && r.BuscaCompras.Dispensa.Any()))
                {
                    worksheet.Cells[row, 1].Value = "DISPENSAS";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Dispensas
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Orgão";
                    worksheet.Cells[row, 3].Value = "Fornecedor";
                    worksheet.Cells[row, 4].Value = "CNPJ";
                    worksheet.Cells[row, 5].Value = "Protocolo";
                    worksheet.Cells[row, 6].Value = "Modalidade";
                    worksheet.Cells[row, 7].Value = "Valor Item";
                    worksheet.Cells[row, 8].Value = "Valor Dispensa";
                    worksheet.Cells[row, 9].Value = "Data Dispensa";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaCompras?.Dispensa != null && resultado.BuscaCompras.Dispensa.Any())
                        {
                            foreach (var contrato in resultado.BuscaCompras.Dispensa)
                            {
                                worksheet.Cells[row, 1].Value = "Dispensa";
                                worksheet.Cells[row, 2].Value = contrato.Orgao;
                                worksheet.Cells[row, 3].Value = contrato.Fornecedor;
                                worksheet.Cells[row, 4].Value = contrato.CNPJ;
                                worksheet.Cells[row, 5].Value = contrato.Protocolo;
                                worksheet.Cells[row, 6].Value = contrato.Modalidade;
                                worksheet.Cells[row, 7].Value = contrato.ValorItem;
                                worksheet.Cells[row, 8].Value = contrato.ValorDispensa;
                                worksheet.Cells[row, 9].Value = contrato.DataDispensa?.ToString("dd/MM/yyyy") ?? "N/A";


                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de Licitações
                if (resultados.Any(r => r.BuscaCompras?.Licitacao != null && r.BuscaCompras.Licitacao.Any()))
                {
                    worksheet.Cells[row, 1].Value = "LICITAÇÕES";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Licitações
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Ano";
                    worksheet.Cells[row, 3].Value = "Orgão";
                    worksheet.Cells[row, 4].Value = "Fornecedor";
                    worksheet.Cells[row, 5].Value = "CNPJ";
                    worksheet.Cells[row, 6].Value = "Protocolo";
                    worksheet.Cells[row, 7].Value = "Valor Homologado";


                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaCompras?.Licitacao != null && resultado.BuscaCompras.Licitacao.Any())
                        {
                            foreach (var contrato in resultado.BuscaCompras.Licitacao)
                            {
                                worksheet.Cells[row, 1].Value = "Licitação";
                                worksheet.Cells[row, 2].Value = contrato.Ano;
                                worksheet.Cells[row, 3].Value = contrato.Orgao;
                                worksheet.Cells[row, 4].Value = contrato.Fornecedor;
                                worksheet.Cells[row, 5].Value = contrato.CNPJ;
                                worksheet.Cells[row, 6].Value = contrato.Protocolo;
                                worksheet.Cells[row, 7].Value = contrato.ValorHomologado;

                                row++;
                            }
                        }
                    }


                }

                // Adicionando a seção de Dividas 
                if (resultados.Any(r => r.BuscaDividas?.DividaFGTS != null && r.BuscaDividas.DividaFGTS.Any()))
                {
                    worksheet.Cells[row, 1].Value = "DIVIDAS FGTS";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de DividasFGTS
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Tipo de Pessoa";
                    worksheet.Cells[row, 3].Value = "Nome Devedor";
                    worksheet.Cells[row, 4].Value = "CNPJ";
                    worksheet.Cells[row, 5].Value = "UFUnidadeResponsavel";
                    worksheet.Cells[row, 6].Value = "Valor Consolidado";


                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaDividas?.DividaFGTS != null && resultado.BuscaDividas.DividaFGTS.Any())
                        {
                            foreach (var divida in resultado.BuscaDividas.DividaFGTS)
                            {
                                worksheet.Cells[row, 1].Value = "DividasFGTS";
                                worksheet.Cells[row, 2].Value = divida.TipoPessoa;
                                worksheet.Cells[row, 3].Value = divida.NomeDevedor;
                                worksheet.Cells[row, 4].Value = divida.CNPJ;
                                worksheet.Cells[row, 5].Value = divida.UFUnidadeResponsavel;
                                worksheet.Cells[row, 7].Value = divida.ValorConsolidado;

                                row++;
                            }
                        }
                    }

                }


                // Adicionando a seção de Dividas 
                if (resultados.Any(r => r.BuscaDividas?.DividaPrevidenciaria != null && r.BuscaDividas.DividaPrevidenciaria.Any()))
                {
                    worksheet.Cells[row, 1].Value = "DIVIDAS PREVIDENCIÁRIAS";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de DividasPrevidenciarias
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Tipo de Pessoa";
                    worksheet.Cells[row, 3].Value = "Nome Devedor";
                    worksheet.Cells[row, 4].Value = "CNPJ";
                    worksheet.Cells[row, 5].Value = "Data Inscrição";
                    worksheet.Cells[row, 6].Value = "Valor Consolidado";


                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaDividas?.DividaPrevidenciaria != null && resultado.BuscaDividas.DividaPrevidenciaria.Any())
                        {
                            foreach (var divida in resultado.BuscaDividas.DividaPrevidenciaria)
                            {
                                worksheet.Cells[row, 1].Value = "DividasPrevidenciaria";
                                worksheet.Cells[row, 2].Value = divida.TipoPessoa;
                                worksheet.Cells[row, 3].Value = divida.NomeDevedor;
                                worksheet.Cells[row, 4].Value = divida.CNPJ;
                                worksheet.Cells[row, 5].Value = divida.DataInscricao;
                                worksheet.Cells[row, 6].Value = divida.ValorConsolidado;

                                row++;
                            }
                        }
                    }

                }


                // Adicionando a seção de Dividas 
                if (resultados.Any(r => r.BuscaDividas?.DividaNaoPrevidenciaria != null && r.BuscaDividas.DividaNaoPrevidenciaria.Any()))
                {
                    worksheet.Cells[row, 1].Value = "DIVIDAS NÃO PREVIDÊNCIÁRIAS";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de DividasNaoPrevidenciarias
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Tipo de Pessoa";
                    worksheet.Cells[row, 3].Value = "Nome Devedor";
                    worksheet.Cells[row, 4].Value = "CNPJ";
                    worksheet.Cells[row, 5].Value = "Data Inscrição";
                    worksheet.Cells[row, 6].Value = "Valor Consolidado";


                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaDividas?.DividaNaoPrevidenciaria != null && resultado.BuscaDividas.DividaNaoPrevidenciaria.Any())
                        {
                            foreach (var divida in resultado.BuscaDividas.DividaNaoPrevidenciaria)
                            {
                                worksheet.Cells[row, 1].Value = "DividasNaoPrevidenciaria";
                                worksheet.Cells[row, 2].Value = divida.TipoPessoa;
                                worksheet.Cells[row, 3].Value = divida.NomeDevedor;
                                worksheet.Cells[row, 4].Value = divida.CNPJ;
                                worksheet.Cells[row, 5].Value = divida.DataInscricao;
                                worksheet.Cells[row, 6].Value = divida.ValorConsolidado;

                                row++;
                            }
                        }
                    }

                }


                // Adicionando a seção de Fazenda 
                if (resultados.Any(r => r.BuscaFazenda?.Jucepar != null && r.BuscaFazenda.Jucepar.Any()))
                {
                    worksheet.Cells[row, 1].Value = "JUNTA COMERCIAL - PR";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Junta Comercial
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Razao Social";
                    worksheet.Cells[row, 3].Value = "CNPJCPF";
                    worksheet.Cells[row, 4].Value = "Nomes Sócios";
                    worksheet.Cells[row, 5].Value = "CPF";
                    worksheet.Cells[row, 6].Value = "Situação";


                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFazenda?.Jucepar != null && resultado.BuscaFazenda.Jucepar.Any())
                        {
                            foreach (var fazenda in resultado.BuscaFazenda.Jucepar)
                            {
                                worksheet.Cells[row, 1].Value = "Junta Comercial";
                                worksheet.Cells[row, 2].Value = fazenda.RazaoSocial;
                                worksheet.Cells[row, 3].Value = fazenda.CNPJ;
                                worksheet.Cells[row, 4].Value = fazenda.NomesSocio;
                                worksheet.Cells[row, 5].Value = fazenda.CPF;
                                worksheet.Cells[row, 6].Value = fazenda.Situacao;

                                row++;
                            }
                        }
                    }

                }

                // Adicionando a seção de Fazenda 
                if (resultados.Any(r => r.BuscaFazenda?.NFEletronica != null && r.BuscaFazenda.NFEletronica.Any()))
                {
                    worksheet.Cells[row, 1].Value = "NOTAS FISCAIS ELETRÔNICAS - PR";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Notas Fiscais Eletronica - PR
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Emitente";
                    worksheet.Cells[row, 3].Value = "CNPJ";
                    worksheet.Cells[row, 4].Value = "Numero NF";
                    worksheet.Cells[row, 5].Value = "Destinatario";
                    worksheet.Cells[row, 6].Value = "CNPJ Destinatario";
                    worksheet.Cells[row, 7].Value = "Valor Nota Fiscal";
                    worksheet.Cells[row, 8].Value = "Data Emissão";
                    worksheet.Cells[row, 9].Value = "Ano";


                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFazenda?.NFEletronica != null && resultado.BuscaFazenda.NFEletronica.Any())
                        {
                            foreach (var fazenda in resultado.BuscaFazenda.NFEletronica)
                            {
                                worksheet.Cells[row, 1].Value = "Notas Fiscais Eletrônicas";
                                worksheet.Cells[row, 2].Value = fazenda.Emitente;
                                worksheet.Cells[row, 3].Value = fazenda.CNPJ;
                                worksheet.Cells[row, 4].Value = fazenda.NumeroNF;
                                worksheet.Cells[row, 5].Value = fazenda.Destinatario;
                                worksheet.Cells[row, 6].Value = fazenda.CNPJDestinatario;
                                worksheet.Cells[row, 7].Value = fazenda.ValorNotaFiscal;
                                worksheet.Cells[row, 8].Value = fazenda.DataEmissao;
                                worksheet.Cells[row, 9].Value = fazenda.Ano;

                                row++;
                            }
                        }
                    }

                }


                // Adicionando a seção de Fazenda 
                if (resultados.Any(r => r.BuscaFazenda?.NFEletronicaFederal != null && r.BuscaFazenda.NFEletronicaFederal.Any()))
                {
                    worksheet.Cells[row, 1].Value = "NOTAS FISCAIS ELETRÔNICAS FEDERAIS - BR";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Notas Fiscais Eletronicas Federais
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Emitente";
                    worksheet.Cells[row, 3].Value = "CNPJ";
                    worksheet.Cells[row, 4].Value = "Numero NF";
                    worksheet.Cells[row, 5].Value = "Destinatario";
                    worksheet.Cells[row, 6].Value = "CNPJ Destinatario";
                    worksheet.Cells[row, 7].Value = "Valor Nota Fiscal";
                    worksheet.Cells[row, 8].Value = "Chave de Acesso";



                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFazenda?.NFEletronicaFederal != null && resultado.BuscaFazenda.NFEletronicaFederal.Any())
                        {
                            foreach (var fazenda in resultado.BuscaFazenda.NFEletronicaFederal)
                            {
                                worksheet.Cells[row, 1].Value = "Notas Fiscais Eletrônicas Federais";
                                worksheet.Cells[row, 2].Value = fazenda.Emitente;
                                worksheet.Cells[row, 3].Value = fazenda.CNPJ;
                                worksheet.Cells[row, 4].Value = fazenda.NumeroNF;
                                worksheet.Cells[row, 5].Value = fazenda.Destinatario;
                                worksheet.Cells[row, 6].Value = fazenda.CNPJDestinatario;
                                worksheet.Cells[row, 7].Value = fazenda.ValorNF;
                                worksheet.Cells[row, 8].Value = fazenda.ChaveAcesso;


                                row++;
                            }
                        }
                    }

                }



                // Adicionando a seção de Estadual 
                if (resultados.Any(r => r.BuscaEstadual?.Ambiental != null && r.BuscaEstadual.Ambiental.Any()))
                {
                    worksheet.Cells[row, 1].Value = "INFRAÇÕES AMBIENTAIS  - PR";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Infração Ambiental
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Infraco";
                    worksheet.Cells[row, 3].Value = "CNPJCPF";
                    worksheet.Cells[row, 4].Value = "Situação";
                    worksheet.Cells[row, 5].Value = "AnoInfração";
                    worksheet.Cells[row, 5].Value = "Valor Autuação";
                    worksheet.Cells[row, 5].Value = "Valor Pago";




                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaEstadual?.Ambiental != null && resultado.BuscaEstadual.Ambiental.Any())
                        {
                            foreach (var estadual in resultado.BuscaEstadual.Ambiental)
                            {
                                worksheet.Cells[row, 1].Value = "Infrações Ambientais";
                                worksheet.Cells[row, 2].Value = estadual.Infrator;
                                worksheet.Cells[row, 3].Value = estadual.CNPJCPF;
                                worksheet.Cells[row, 4].Value = estadual.Situacao;
                                worksheet.Cells[row, 5].Value = estadual.AnoInfracao;
                                worksheet.Cells[row, 6].Value = estadual.ValorAutuacao;
                                worksheet.Cells[row, 7].Value = estadual.ValorOficioPago;



                                row++;
                            }
                        }
                    }

                }


                // Adicionando a seção de Estadual 
                if (resultados.Any(r => r.BuscaEstadual?.PADV != null && r.BuscaEstadual.PADV.Any()))
                {
                    worksheet.Cells[row, 1].Value = "PADV - PR";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Infração Ambiental
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Razão Social";
                    worksheet.Cells[row, 3].Value = "Agência";
                    worksheet.Cells[row, 4].Value = "CNPJ";
                    worksheet.Cells[row, 5].Value = "Numero PADV";
                    worksheet.Cells[row, 5].Value = "Orgão Pagador";
                    worksheet.Cells[row, 5].Value = "Valor Pago";




                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaEstadual?.PADV != null && resultado.BuscaEstadual.PADV.Any())
                        {
                            foreach (var estadual in resultado.BuscaEstadual.PADV)
                            {
                                worksheet.Cells[row, 1].Value = "PADV";
                                worksheet.Cells[row, 2].Value = estadual.RazaoSocial;
                                worksheet.Cells[row, 3].Value = estadual.Agencia;
                                worksheet.Cells[row, 4].Value = estadual.CNPJExecutor;
                                worksheet.Cells[row, 5].Value = estadual.NumeroPADV;
                                worksheet.Cells[row, 6].Value = estadual.OrgaoPagador;
                                worksheet.Cells[row, 7].Value = estadual.ValorPago;

                                row++;
                            }
                        }
                    }

                }


                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.AcordosLeniencia != null && r.BuscaFederal.AcordosLeniencia.Any()))
                {
                    worksheet.Cells[row, 1].Value = " Acordo Leniência - PR";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Infração Ambiental
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Razão Social";
                    worksheet.Cells[row, 3].Value = "CNPJ";
                    worksheet.Cells[row, 4].Value = "Numero Processo";
                    worksheet.Cells[row, 5].Value = "Data Inicio Acordo";
                    worksheet.Cells[row, 6].Value = "Data Fim Acordo Pago";
                    worksheet.Cells[row, 7].Value = "Situação";
                    worksheet.Cells[row, 8].Value = "Efeitos";
                    worksheet.Cells[row, 9].Value = "Termos Acordo";




                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.AcordosLeniencia != null && resultado.BuscaFederal.AcordosLeniencia.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.AcordosLeniencia)
                            {
                                worksheet.Cells[row, 1].Value = "Acordos Leniência";
                                worksheet.Cells[row, 2].Value = federal.RazaoSocial;
                                worksheet.Cells[row, 3].Value = federal.CNPJ;
                                worksheet.Cells[row, 4].Value = federal.NumeroProcesso;
                                worksheet.Cells[row, 5].Value = federal.DataInicioAcordo;
                                worksheet.Cells[row, 6].Value = federal.DataFimAcordo;
                                worksheet.Cells[row, 7].Value = federal.Situacao;
                                worksheet.Cells[row, 8].Value = federal.Efeitos;
                                worksheet.Cells[row, 9].Value = federal.TermosAcordo;

                                row++;
                            }
                        }
                    }

                }

                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.Aeronave != null && r.BuscaFederal.Aeronave.Any()))
                {
                    worksheet.Cells[row, 1].Value = " Aeronaves - BR";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Aeronaves
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Proprietário";
                    worksheet.Cells[row, 3].Value = "CNPJCPF";
                    worksheet.Cells[row, 4].Value = "Marca";
                    worksheet.Cells[row, 5].Value = "Operador";
                    worksheet.Cells[row, 6].Value = "CPFCGC";





                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.Aeronave != null && resultado.BuscaFederal.Aeronave.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.Aeronave)
                            {
                                worksheet.Cells[row, 1].Value = "Aeronaves";
                                worksheet.Cells[row, 2].Value = federal.Proprietario;
                                worksheet.Cells[row, 3].Value = federal.CNPJCPF;
                                worksheet.Cells[row, 4].Value = federal.Marca;
                                worksheet.Cells[row, 5].Value = federal.Operador;
                                worksheet.Cells[row, 6].Value = federal.CPFCGC;


                                row++;
                            }
                        }
                    }

                }


                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.BeneficioContinuo != null && r.BuscaFederal.BeneficioContinuo.Any()))
                {
                    worksheet.Cells[row, 1].Value = " BENEFICIOS CONTINUOS - BPC ";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de Benficio Continuo
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Beneficiário";
                    worksheet.Cells[row, 3].Value = "CPF";
                    worksheet.Cells[row, 4].Value = "Municipio";
                    worksheet.Cells[row, 5].Value = "UF";
                    worksheet.Cells[row, 6].Value = "Numero Beneficio";
                    worksheet.Cells[row, 7].Value = "Ano";
                    worksheet.Cells[row, 8].Value = "Mes";
                    worksheet.Cells[row, 9].Value = "Valor Parcela";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.BeneficioContinuo != null && resultado.BuscaFederal.BeneficioContinuo.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.BeneficioContinuo)
                            {
                                worksheet.Cells[row, 1].Value = "Beneficios Continuos";
                                worksheet.Cells[row, 2].Value = federal.NomeBeneficiario;
                                worksheet.Cells[row, 3].Value = federal.CNPJCPF;
                                worksheet.Cells[row, 4].Value = federal.NomeMunicipio;
                                worksheet.Cells[row, 5].Value = federal.UFMunicipio;
                                worksheet.Cells[row, 6].Value = federal.NumeroBeneficio;
                                worksheet.Cells[row, 7].Value = federal.Ano;
                                worksheet.Cells[row, 8].Value = federal.MesReferencia;
                                worksheet.Cells[row, 9].Value = federal.ValorParcela;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.BolsaFamilia != null && r.BuscaFederal.BolsaFamilia.Any()))
                {
                    worksheet.Cells[row, 1].Value = "BOLSA FAMILIA";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de Bolsa Familia
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Nome";
                    worksheet.Cells[row, 3].Value = "CPF";
                    worksheet.Cells[row, 4].Value = "Municipio";
                    worksheet.Cells[row, 5].Value = "UF";
                    worksheet.Cells[row, 6].Value = "Ano";
                    worksheet.Cells[row, 7].Value = "Mes";
                    worksheet.Cells[row, 8].Value = "Valor Parcela";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.BolsaFamilia != null && resultado.BuscaFederal.BolsaFamilia.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.BolsaFamilia)
                            {
                                worksheet.Cells[row, 1].Value = "Bolsa Familia";
                                worksheet.Cells[row, 2].Value = federal.Nome;
                                worksheet.Cells[row, 3].Value = federal.CPF;
                                worksheet.Cells[row, 4].Value = federal.NomeMunicipio;
                                worksheet.Cells[row, 5].Value = federal.UF;
                                worksheet.Cells[row, 6].Value = federal.AnoReferencia;
                                worksheet.Cells[row, 7].Value = federal.MesReferencia;
                                worksheet.Cells[row, 8].Value = federal.Valor;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.Ceis != null && r.BuscaFederal.Ceis.Any()))
                {
                    worksheet.Cells[row, 1].Value = "CEIS";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de Ceis
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Razão Social";
                    worksheet.Cells[row, 3].Value = "CNPJCPF";
                    worksheet.Cells[row, 4].Value = "Tipo Pessoa";
                    worksheet.Cells[row, 5].Value = "Tipo Sanção";
                    worksheet.Cells[row, 6].Value = "Data Inicio Sanção";
                    worksheet.Cells[row, 7].Value = "Data Fim Sanção";


                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.Ceis != null && resultado.BuscaFederal.Ceis.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.Ceis)
                            {
                                worksheet.Cells[row, 1].Value = "Ceis";
                                worksheet.Cells[row, 2].Value = federal.RazaoSocialReceita;
                                worksheet.Cells[row, 3].Value = federal.CNPJCPF;
                                worksheet.Cells[row, 4].Value = federal.TipoPessoa;
                                worksheet.Cells[row, 5].Value = federal.TipoSancao;
                                worksheet.Cells[row, 6].Value = federal.DTInicioSancao;
                                worksheet.Cells[row, 7].Value = federal.DTFimSancao;
                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.Cepim != null && r.BuscaFederal.Cepim.Any()))
                {
                    worksheet.Cells[row, 1].Value = "CEPIM";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de Cepim
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Nome";
                    worksheet.Cells[row, 3].Value = "CNPJ";
                    worksheet.Cells[row, 4].Value = "Numero Convênio";
                    worksheet.Cells[row, 5].Value = "Orgão";
                    worksheet.Cells[row, 6].Value = "Impedimento";



                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.Cepim != null && resultado.BuscaFederal.Cepim.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.Cepim)
                            {
                                worksheet.Cells[row, 1].Value = "Cepim";
                                worksheet.Cells[row, 2].Value = federal.Nome;
                                worksheet.Cells[row, 3].Value = federal.CNPJ;
                                worksheet.Cells[row, 4].Value = federal.NroConvenio;
                                worksheet.Cells[row, 5].Value = federal.Orgao;
                                worksheet.Cells[row, 6].Value = federal.Impedimento;

                                row++;
                            }
                        }
                    }
                }


                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.Cnep != null && r.BuscaFederal.Cnep.Any()))
                {
                    worksheet.Cells[row, 1].Value = "CNEP";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de CNEP
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Razão Social";
                    worksheet.Cells[row, 3].Value = "CNPJCPF";
                    worksheet.Cells[row, 4].Value = "Numero Processo";
                    worksheet.Cells[row, 5].Value = "Data Inicio Sanção";
                    worksheet.Cells[row, 6].Value = "Data Fim Sanção";
                    worksheet.Cells[row, 7].Value = "Fundamentação Legal";



                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.Cnep != null && resultado.BuscaFederal.Cnep.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.Cnep)
                            {
                                worksheet.Cells[row, 1].Value = "Cnep";
                                worksheet.Cells[row, 2].Value = federal.RazaoSocial;
                                worksheet.Cells[row, 3].Value = federal.CNPJCPF;
                                worksheet.Cells[row, 4].Value = federal.NroProcesso;
                                worksheet.Cells[row, 5].Value = federal.DataInicioSancao;
                                worksheet.Cells[row, 6].Value = federal.DataFimSancao;
                                worksheet.Cells[row, 7].Value = federal.FundamentacaoLegal;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.ExpulsoFederal != null && r.BuscaFederal.ExpulsoFederal.Any()))
                {
                    worksheet.Cells[row, 1].Value = "EXPULSO FEDERAL";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de EXPULSO FEDERAL
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Nome";
                    worksheet.Cells[row, 3].Value = "CPF";
                    worksheet.Cells[row, 4].Value = "Punição";
                    worksheet.Cells[row, 5].Value = "Numero do Processo";
                    worksheet.Cells[row, 6].Value = "Data da Publicação";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.ExpulsoFederal != null && resultado.BuscaFederal.ExpulsoFederal.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.ExpulsoFederal)
                            {
                                worksheet.Cells[row, 1].Value = "Expulso Federal";
                                worksheet.Cells[row, 2].Value = federal.Nome;
                                worksheet.Cells[row, 3].Value = federal.CNPJCPF;
                                worksheet.Cells[row, 4].Value = federal.Punicao;
                                worksheet.Cells[row, 5].Value = federal.NumeroProcesso;
                                worksheet.Cells[row, 6].Value = federal.DataPublicacao;


                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.PEP != null && r.BuscaFederal.PEP.Any()))
                {
                    worksheet.Cells[row, 1].Value = "PESSOA EXPOSTA POLITICAMENTE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de PEP
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Nome";
                    worksheet.Cells[row, 3].Value = "CPF";
                    worksheet.Cells[row, 4].Value = "Função";
                    worksheet.Cells[row, 5].Value = "Órgão";
                    worksheet.Cells[row, 6].Value = "Data Inicio";
                    worksheet.Cells[row, 7].Value = "Data Fim";
                    worksheet.Cells[row, 8].Value = "Data Fim Carência";
                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.PEP != null && resultado.BuscaFederal.PEP.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.PEP)
                            {
                                worksheet.Cells[row, 1].Value = "Expulso Federal";
                                worksheet.Cells[row, 2].Value = federal.Nome;
                                worksheet.Cells[row, 3].Value = federal.CPF;
                                worksheet.Cells[row, 4].Value = federal.Funcao;
                                worksheet.Cells[row, 5].Value = federal.Orgao;
                                worksheet.Cells[row, 6].Value = federal.DataInicio;
                                worksheet.Cells[row, 7].Value = federal.DataFim;
                                worksheet.Cells[row, 8].Value = federal.DataFimCarencia;

                                row++;
                            }
                        }
                    }
                }


                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.SeguroDefeso != null && r.BuscaFederal.SeguroDefeso.Any()))
                {
                    worksheet.Cells[row, 1].Value = "SEGURO DEFESO";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de SEGURO DEFESO
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Nome";
                    worksheet.Cells[row, 3].Value = "CPF";
                    worksheet.Cells[row, 4].Value = "Municipio";
                    worksheet.Cells[row, 5].Value = "UF";
                    worksheet.Cells[row, 6].Value = "Ano";
                    worksheet.Cells[row, 7].Value = "Mes";
                    worksheet.Cells[row, 8].Value = "Valor Parcela";
                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.SeguroDefeso != null && resultado.BuscaFederal.SeguroDefeso.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.SeguroDefeso)
                            {
                                worksheet.Cells[row, 1].Value = "Seguro Defeso";
                                worksheet.Cells[row, 2].Value = federal.NomeFavorecido;
                                worksheet.Cells[row, 3].Value = federal.CPF;
                                worksheet.Cells[row, 4].Value = federal.NomeMunicipio;
                                worksheet.Cells[row, 5].Value = federal.UF;
                                worksheet.Cells[row, 6].Value = federal.Ano;
                                worksheet.Cells[row, 7].Value = federal.Mes;
                                worksheet.Cells[row, 8].Value = federal.ValorParcela;

                                row++;
                            }
                        }
                    }
                }


                // Adicionando a seção de Federal 
                if (resultados.Any(r => r.BuscaFederal?.TrabalhoEscravo != null && r.BuscaFederal.TrabalhoEscravo.Any()))
                {
                    worksheet.Cells[row, 1].Value = "TRABALHO ESCRAVO";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de TRABALHO ESCRAVO
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Empregador";
                    worksheet.Cells[row, 3].Value = "CNPJCPF";
                    worksheet.Cells[row, 4].Value = "Estabelecimento";
                    worksheet.Cells[row, 5].Value = "Ano";
                    worksheet.Cells[row, 6].Value = "UF";
                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaFederal?.TrabalhoEscravo != null && resultado.BuscaFederal.TrabalhoEscravo.Any())
                        {
                            foreach (var federal in resultado.BuscaFederal.TrabalhoEscravo)
                            {
                                worksheet.Cells[row, 1].Value = "Seguro Defeso";
                                worksheet.Cells[row, 2].Value = federal.Empregador;
                                worksheet.Cells[row, 3].Value = federal.CNPJCPF;
                                worksheet.Cells[row, 4].Value = federal.Estabelecimento;
                                worksheet.Cells[row, 5].Value = federal.Ano;
                                worksheet.Cells[row, 6].Value = federal.UF;

                                row++;
                            }
                        }
                    }
                }


                // Adicionando a seção de TCE 
                if (resultados.Any(r => r.BuscaTCE?.CNPJRestrito != null && r.BuscaTCE.CNPJRestrito.Any()))
                {
                    worksheet.Cells[row, 1].Value = "CNPJ Restrito TCE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de CNPJ RESTRITO TCE 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Razão Social";
                    worksheet.Cells[row, 3].Value = "CNPJ";
                    worksheet.Cells[row, 4].Value = "Data Inicio";
                    worksheet.Cells[row, 5].Value = "Data Fim";
                    worksheet.Cells[row, 6].Value = "Tipo Sanção";
                    worksheet.Cells[row, 7].Value = "Situação";
                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTCE?.CNPJRestrito != null && resultado.BuscaTCE.CNPJRestrito.Any())
                        {
                            foreach (var tce in resultado.BuscaTCE.CNPJRestrito)
                            {
                                worksheet.Cells[row, 1].Value = "CNPJ Restrito TCE";
                                worksheet.Cells[row, 2].Value = tce.NomeRazaoSocial;
                                worksheet.Cells[row, 3].Value = tce.CNPJCPF;
                                worksheet.Cells[row, 4].Value = tce.DataInicio;
                                worksheet.Cells[row, 5].Value = tce.DataFim;
                                worksheet.Cells[row, 6].Value = tce.TipoSancao;
                                worksheet.Cells[row, 7].Value = tce.Situacao;

                                row++;
                            }
                        }
                    }
                }


                // Adicionando a seção de TCE 
                if (resultados.Any(r => r.BuscaTCE?.CPFRestrito != null && r.BuscaTCE.CPFRestrito.Any()))
                {
                    worksheet.Cells[row, 1].Value = "CPF Restrito TCE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de CPF RESTRITO TCE 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Razão Social";
                    worksheet.Cells[row, 3].Value = "CNPJ";
                    worksheet.Cells[row, 4].Value = "Data Inicio";
                    worksheet.Cells[row, 5].Value = "Data Fim";
                    worksheet.Cells[row, 6].Value = "Tipo Sanção";
                    worksheet.Cells[row, 7].Value = "Situação";
                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTCE?.CPFRestrito != null && resultado.BuscaTCE.CPFRestrito.Any())
                        {
                            foreach (var tce in resultado.BuscaTCE.CPFRestrito)
                            {
                                worksheet.Cells[row, 1].Value = "CNPJ Restrito TCE";
                                worksheet.Cells[row, 2].Value = tce.NomeRazaoSocial;
                                worksheet.Cells[row, 3].Value = tce.CNPJCPF;
                                worksheet.Cells[row, 4].Value = tce.DataInicio;
                                worksheet.Cells[row, 5].Value = tce.DataFim;
                                worksheet.Cells[row, 6].Value = tce.TipoSancao;
                                worksheet.Cells[row, 7].Value = tce.Situacao;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de TCE 
                if (resultados.Any(r => r.BuscaTCE?.Inadimplente != null && r.BuscaTCE.Inadimplente.Any()))
                {
                    worksheet.Cells[row, 1].Value = "INADIMPLENTE TCE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de INADIMPLENTES TCE 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Devedor";
                    worksheet.Cells[row, 3].Value = "CNPJ";
                    worksheet.Cells[row, 4].Value = "CPF";
                    worksheet.Cells[row, 5].Value = "Credor";
                    worksheet.Cells[row, 6].Value = "Processo";
                    worksheet.Cells[row, 7].Value = "Tipo Sanção";
                    worksheet.Cells[row, 8].Value = "Referência";
                    worksheet.Cells[row, 9].Value = "Valor";
                    worksheet.Cells[row, 10].Value = "Valor Recolhido";
                    worksheet.Cells[row, 11].Value = "Saldo Devedor";
                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTCE?.Inadimplente != null && resultado.BuscaTCE.Inadimplente.Any())
                        {
                            foreach (var tce in resultado.BuscaTCE.Inadimplente)
                            {
                                worksheet.Cells[row, 1].Value = "INADIMPLENTES TCE";
                                worksheet.Cells[row, 2].Value = tce.Devedor;
                                worksheet.Cells[row, 3].Value = tce.CNPJ;
                                worksheet.Cells[row, 4].Value = tce.CPF;
                                worksheet.Cells[row, 5].Value = tce.Credor;
                                worksheet.Cells[row, 6].Value = tce.Processo;
                                worksheet.Cells[row, 7].Value = tce.TipoSancao;
                                worksheet.Cells[row, 8].Value = tce.Referencia;
                                worksheet.Cells[row, 9].Value = tce.Valor;
                                worksheet.Cells[row, 10].Value = tce.ValorRecolhido;
                                worksheet.Cells[row, 11].Value = tce.SaldoDevedor;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de TCE 
                if (resultados.Any(r => r.BuscaTCE?.Irregularidade != null && r.BuscaTCE.Irregularidade.Any()))
                {
                    worksheet.Cells[row, 1].Value = "IRREGULARIDADES TCE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de INADIMPLENTES TCE 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Nome";
                    worksheet.Cells[row, 3].Value = "CPF";
                    worksheet.Cells[row, 4].Value = "Cargo";
                    worksheet.Cells[row, 5].Value = "Inicio Vigência";
                    worksheet.Cells[row, 6].Value = "Termino Vigência";
                    worksheet.Cells[row, 7].Value = "Data Publicação";
                    worksheet.Cells[row, 8].Value = "Descrição";
                    worksheet.Cells[row, 9].Value = "CNPJ";
                    worksheet.Cells[row, 10].Value = "Entidade";
                    worksheet.Cells[row, 11].Value = "Processo";
                    worksheet.Cells[row, 12].Value = "Decisão";
                    worksheet.Cells[row, 13].Value = "Tipo";
                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTCE?.Irregularidade != null && resultado.BuscaTCE.Irregularidade.Any())
                        {
                            foreach (var tce in resultado.BuscaTCE.Irregularidade)
                            {
                                worksheet.Cells[row, 1].Value = "IRREGULARIDADES TCE";
                                worksheet.Cells[row, 2].Value = tce.Nome;
                                worksheet.Cells[row, 3].Value = tce.CPF;
                                worksheet.Cells[row, 4].Value = tce.Cargo;
                                worksheet.Cells[row, 5].Value = tce.InicioVigencia;
                                worksheet.Cells[row, 6].Value = tce.TerminoVigencia;
                                worksheet.Cells[row, 7].Value = tce.DataPublicacao;
                                worksheet.Cells[row, 8].Value = tce.Descricao;
                                worksheet.Cells[row, 9].Value = tce.CNPJ;
                                worksheet.Cells[row, 10].Value = tce.Entidade;
                                worksheet.Cells[row, 11].Value = tce.Processo;
                                worksheet.Cells[row, 12].Value = tce.Tipo;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de TCU 
                if (resultados.Any(r => r.BuscaTCU?.ContaEleitoralIrregular != null && r.BuscaTCU.ContaEleitoralIrregular.Any()))
                {
                    worksheet.Cells[row, 1].Value = "CONTA ELEITORAL IRREGULAR  TCU";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de CONTA ELEITORAL IRREGULAR TCU 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Nome";
                    worksheet.Cells[row, 3].Value = "CPF";
                    worksheet.Cells[row, 4].Value = "Cargo ou Função";
                    worksheet.Cells[row, 5].Value = "Municipio";
                    worksheet.Cells[row, 6].Value = "UF";
                    worksheet.Cells[row, 7].Value = "Deliberação";
                    worksheet.Cells[row, 8].Value = "Data Julgado";
                    worksheet.Cells[row, 9].Value = "Data Final";
                    worksheet.Cells[row, 10].Value = "Processo";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTCU?.ContaEleitoralIrregular != null && resultado.BuscaTCU.ContaEleitoralIrregular.Any())
                        {
                            foreach (var tcu in resultado.BuscaTCU.ContaEleitoralIrregular)
                            {
                                worksheet.Cells[row, 1].Value = " CONTA ELEITORAL IRREGULAR  TCU";
                                worksheet.Cells[row, 2].Value = tcu.Nome;
                                worksheet.Cells[row, 3].Value = tcu.CPF;
                                worksheet.Cells[row, 4].Value = tcu.CargoFuncao;
                                worksheet.Cells[row, 5].Value = tcu.Municipio;
                                worksheet.Cells[row, 6].Value = tcu.UF;
                                worksheet.Cells[row, 7].Value = tcu.Deliberacao;
                                worksheet.Cells[row, 8].Value = tcu.DataJulgado;
                                worksheet.Cells[row, 9].Value = tcu.DataFinal;
                                worksheet.Cells[row, 10].Value = tcu.Processo;

                                row++;
                            }
                        }
                    }
                }


                // Adicionando a seção de TCU 
                if (resultados.Any(r => r.BuscaTCU?.ContaIrregular != null && r.BuscaTCU.ContaIrregular.Any()))
                {
                    worksheet.Cells[row, 1].Value = "CONTA IRREGULAR  TCU";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de CONTA IRREGULAR  TCU 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Nome";
                    worksheet.Cells[row, 3].Value = "CNPJCPF";
                    worksheet.Cells[row, 4].Value = "Municipio";
                    worksheet.Cells[row, 5].Value = "UF";
                    worksheet.Cells[row, 6].Value = "Deliberação";
                    worksheet.Cells[row, 7].Value = "Data Julgado";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTCU?.ContaIrregular != null && resultado.BuscaTCU.ContaIrregular.Any())
                        {
                            foreach (var tcu in resultado.BuscaTCU.ContaIrregular)
                            {
                                worksheet.Cells[row, 1].Value = " CONTA IRREGULAR  TCU";
                                worksheet.Cells[row, 2].Value = tcu.Nome;
                                worksheet.Cells[row, 3].Value = tcu.CNPJCPF;
                                worksheet.Cells[row, 4].Value = tcu.Municipio;
                                worksheet.Cells[row, 5].Value = tcu.UF;
                                worksheet.Cells[row, 6].Value = tcu.Deliberacao;
                                worksheet.Cells[row, 7].Value = tcu.DataJulgado;

                                row++;
                            }
                        }
                    }
                }


                // Adicionando a seção de TCU 
                if (resultados.Any(r => r.BuscaTCU?.Inabilitado != null && r.BuscaTCU.Inabilitado.Any()))
                {
                    worksheet.Cells[row, 1].Value = "INABILITADOS TCU";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de INABILITADOS TCU 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Nome";
                    worksheet.Cells[row, 3].Value = "CPF";
                    worksheet.Cells[row, 4].Value = "Processo";
                    worksheet.Cells[row, 5].Value = "Deliberação";
                    worksheet.Cells[row, 6].Value = "Data Final";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTCU?.Inabilitado != null && resultado.BuscaTCU.Inabilitado.Any())
                        {
                            foreach (var tcu in resultado.BuscaTCU.Inabilitado)
                            {
                                worksheet.Cells[row, 1].Value = " INABILITADOS  TCU";
                                worksheet.Cells[row, 2].Value = tcu.Nome;
                                worksheet.Cells[row, 3].Value = tcu.CNPJCPF;
                                worksheet.Cells[row, 4].Value = tcu.Processo;
                                worksheet.Cells[row, 5].Value = tcu.Deliberacao;
                                worksheet.Cells[row, 6].Value = tcu.DataFinal;

                                row++;
                            }
                        }
                    }
                }



                // Adicionando a seção de TCU 
                if (resultados.Any(r => r.BuscaTCU?.Inidoneo != null && r.BuscaTCU.Inidoneo.Any()))
                {
                    worksheet.Cells[row, 1].Value = "INIDONEOS TCU";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de INIDONEOS TCU 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Nome";
                    worksheet.Cells[row, 3].Value = "CNPJ";
                    worksheet.Cells[row, 4].Value = "Processo";
                    worksheet.Cells[row, 5].Value = "Deliberação";
                    worksheet.Cells[row, 6].Value = "Data Final";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTCU?.Inidoneo != null && resultado.BuscaTCU.Inidoneo.Any())
                        {
                            foreach (var tcu in resultado.BuscaTCU.Inidoneo)
                            {
                                worksheet.Cells[row, 1].Value = " INIDONEOS TCU";
                                worksheet.Cells[row, 2].Value = tcu.Nome;
                                worksheet.Cells[row, 3].Value = tcu.CNPJCPF;
                                worksheet.Cells[row, 4].Value = tcu.Processo;
                                worksheet.Cells[row, 5].Value = tcu.Deliberacao;
                                worksheet.Cells[row, 6].Value = tcu.DataFinal;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de TSE 
                if (resultados.Any(r => r.BuscaTSE?.DoacaoCandidato != null && r.BuscaTSE.DoacaoCandidato.Any()))
                {
                    worksheet.Cells[row, 1].Value = "DOAÇÕES CANDIDATOS TSE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de DOAÇÃO CANDIDATOS TSE 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Ano Eleição";
                    worksheet.Cells[row, 3].Value = "Municipio";
                    worksheet.Cells[row, 4].Value = "UF";
                    worksheet.Cells[row, 5].Value = "Nome Candidato";
                    worksheet.Cells[row, 6].Value = "CPF Candidato";
                    worksheet.Cells[row, 7].Value = "Cargo";
                    worksheet.Cells[row, 8].Value = "Nome Partido";
                    worksheet.Cells[row, 9].Value = "Nome Doador";
                    worksheet.Cells[row, 10].Value = "CPF Doador";
                    worksheet.Cells[row, 11].Value = "Descrição Receita";
                    worksheet.Cells[row, 12].Value = "Valor Doação";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTSE?.DoacaoCandidato != null && resultado.BuscaTSE.DoacaoCandidato.Any())
                        {
                            foreach (var tse in resultado.BuscaTSE.DoacaoCandidato)
                            {
                                worksheet.Cells[row, 1].Value = " DOAÇÕES CANDIDATOS TSE";
                                worksheet.Cells[row, 2].Value = tse.AnoEleicao;
                                worksheet.Cells[row, 3].Value = tse.Municipio;
                                worksheet.Cells[row, 4].Value = tse.UF;
                                worksheet.Cells[row, 5].Value = tse.NomeCandidato;
                                worksheet.Cells[row, 6].Value = tse.CPFCandidato;
                                worksheet.Cells[row, 7].Value = tse.Cargo;
                                worksheet.Cells[row, 8].Value = tse.NomePartido;
                                worksheet.Cells[row, 9].Value = tse.NomeDoador;
                                worksheet.Cells[row, 10].Value = tse.CPFDoador;
                                worksheet.Cells[row, 11].Value = tse.DescricaoReceita;
                                worksheet.Cells[row, 12].Value = tse.ValorDoacao;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de TSE 
                if (resultados.Any(r => r.BuscaTSE?.DoacaoGeral != null && r.BuscaTSE.DoacaoGeral.Any()))
                {
                    worksheet.Cells[row, 1].Value = "DOAÇÕES GERAL TSE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de DOAÇÃO GERAL TSE 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Ano Eleição";
                    worksheet.Cells[row, 3].Value = "Nome";
                    worksheet.Cells[row, 4].Value = "CPF";
                    worksheet.Cells[row, 5].Value = "Nome Partido";
                    worksheet.Cells[row, 6].Value = "Sigla Partido";
                    worksheet.Cells[row, 7].Value = "Descrição Receita";
                    worksheet.Cells[row, 8].Value = "Valor Doação";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTSE?.DoacaoGeral != null && resultado.BuscaTSE.DoacaoGeral.Any())
                        {
                            foreach (var tse in resultado.BuscaTSE.DoacaoGeral)
                            {
                                worksheet.Cells[row, 1].Value = "DOAÇÕES GERAL TSE";
                                worksheet.Cells[row, 2].Value = tse.AnoEleicao;
                                worksheet.Cells[row, 3].Value = tse.Nome;
                                worksheet.Cells[row, 4].Value = tse.CPF;
                                worksheet.Cells[row, 5].Value = tse.NomePartido;
                                worksheet.Cells[row, 6].Value = tse.SiglaPartido;
                                worksheet.Cells[row, 7].Value = tse.DescricaoReceita;
                                worksheet.Cells[row, 8].Value = tse.ValorDoacao;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de TSE 
                if (resultados.Any(r => r.BuscaTSE?.DoacaoPartidoGeral != null && r.BuscaTSE.DoacaoPartidoGeral.Any()))
                {
                    worksheet.Cells[row, 1].Value = "DOAÇÕES PARTIDOS GERAL TSE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de DOAÇÃO PARTIDOS GERAL TSE 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Ano Eleição";
                    worksheet.Cells[row, 3].Value = "UF";
                    worksheet.Cells[row, 4].Value = "Nome";
                    worksheet.Cells[row, 5].Value = "CPF";
                    worksheet.Cells[row, 6].Value = "Nome Partido";
                    worksheet.Cells[row, 7].Value = "Descrição Receita";
                    worksheet.Cells[row, 8].Value = "Valor Doação";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTSE?.DoacaoPartidoGeral != null && resultado.BuscaTSE.DoacaoPartidoGeral.Any())
                        {
                            foreach (var tse in resultado.BuscaTSE.DoacaoPartidoGeral)
                            {
                                worksheet.Cells[row, 1].Value = "DOAÇÕES PARTIDOS GERAL TSE";
                                worksheet.Cells[row, 2].Value = tse.AnoEleicao;
                                worksheet.Cells[row, 3].Value = tse.UF;
                                worksheet.Cells[row, 4].Value = tse.Nome;
                                worksheet.Cells[row, 5].Value = tse.CPF;
                                worksheet.Cells[row, 6].Value = tse.NomePartido;
                                worksheet.Cells[row, 7].Value = tse.DescricaoReceita;
                                worksheet.Cells[row, 8].Value = tse.ValorDoacao;

                                row++;
                            }
                        }
                    }
                }


                // Adicionando a seção de TSE 
                if (resultados.Any(r => r.BuscaTSE?.DoacaoPartido != null && r.BuscaTSE.DoacaoPartido.Any()))
                {
                    worksheet.Cells[row, 1].Value = "DOAÇÕES PARTIDOS TSE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de DOAÇÃO PARTIDOS TSE 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Ano Eleição";
                    worksheet.Cells[row, 3].Value = "UF";
                    worksheet.Cells[row, 4].Value = "Nome Partido";
                    worksheet.Cells[row, 5].Value = "Nome Doador";
                    worksheet.Cells[row, 6].Value = "CPF Doador";
                    worksheet.Cells[row, 7].Value = "Descriçao Receita";
                    worksheet.Cells[row, 8].Value = "Descrição Espécia Receita";
                    worksheet.Cells[row, 9].Value = "Valor Receita";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTSE?.DoacaoPartido != null && resultado.BuscaTSE.DoacaoPartido.Any())
                        {
                            foreach (var tse in resultado.BuscaTSE.DoacaoPartido)
                            {
                                worksheet.Cells[row, 1].Value = "DOAÇÕES PARTIDOS TSE";
                                worksheet.Cells[row, 2].Value = tse.AnoEleicao;
                                worksheet.Cells[row, 3].Value = tse.UF;
                                worksheet.Cells[row, 4].Value = tse.NomePartido;
                                worksheet.Cells[row, 5].Value = tse.NomeDoador;
                                worksheet.Cells[row, 6].Value = tse.CPFDoador;
                                worksheet.Cells[row, 7].Value = tse.DescricaoReceita;
                                worksheet.Cells[row, 8].Value = tse.DescricaoEspecieReceita;
                                worksheet.Cells[row, 9].Value = tse.ValorReceita;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de TSE 
                if (resultados.Any(r => r.BuscaTSE?.FornecedorCandidato != null && r.BuscaTSE.FornecedorCandidato.Any()))
                {
                    worksheet.Cells[row, 1].Value = "FORNECEDOR CANDIDATOS TSE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de FORNECEDOR CANDIDATOS TSE 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Ano Eleição";
                    worksheet.Cells[row, 3].Value = "Municipio";
                    worksheet.Cells[row, 4].Value = "Candidato";
                    worksheet.Cells[row, 5].Value = "CPF Candidato";
                    worksheet.Cells[row, 6].Value = "Descrição Cargo";
                    worksheet.Cells[row, 7].Value = "Partido";
                    worksheet.Cells[row, 8].Value = "CNPJCPF";
                    worksheet.Cells[row, 9].Value = "Fornecedor";
                    worksheet.Cells[row, 10].Value = "Valor Receita";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTSE?.FornecedorCandidato != null && resultado.BuscaTSE.FornecedorCandidato.Any())
                        {
                            foreach (var tse in resultado.BuscaTSE.FornecedorCandidato)
                            {
                                worksheet.Cells[row, 1].Value = "FORNECEDOR CANDIDATOS TSE";
                                worksheet.Cells[row, 2].Value = tse.AnoEleicao;
                                worksheet.Cells[row, 3].Value = tse.Municipio;
                                worksheet.Cells[row, 4].Value = tse.Candidato;
                                worksheet.Cells[row, 5].Value = tse.CPFCandidato;
                                worksheet.Cells[row, 6].Value = tse.DescricaoCargo;
                                worksheet.Cells[row, 7].Value = tse.Partido;
                                worksheet.Cells[row, 8].Value = tse.CNPJCPF;
                                worksheet.Cells[row, 9].Value = tse.Fornecedor;
                                worksheet.Cells[row, 10].Value = tse.ValorDespesas;

                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de TSE 
                if (resultados.Any(r => r.BuscaTSE?.FornecedorPartido != null && r.BuscaTSE.FornecedorPartido.Any()))
                {
                    worksheet.Cells[row, 1].Value = "FORNECEDOR PARTIDOS TSE";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;
                    // Cabeçalho de FORNECEDOR PARTIDOS TSE 
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Ano Eleição";
                    worksheet.Cells[row, 3].Value = "Municipio";
                    worksheet.Cells[row, 4].Value = "UF";
                    worksheet.Cells[row, 5].Value = "Partido";
                    worksheet.Cells[row, 6].Value = "CNPJCPF";
                    worksheet.Cells[row, 7].Value = "Fornecedor";
                    worksheet.Cells[row, 8].Value = "Descriçao Despesas";
                    worksheet.Cells[row, 9].Value = "Partido Fornecedor";
                    worksheet.Cells[row, 10].Value = "Valor Receita";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaTSE?.FornecedorPartido != null && resultado.BuscaTSE.FornecedorPartido.Any())
                        {
                            foreach (var tse in resultado.BuscaTSE.FornecedorPartido)
                            {
                                worksheet.Cells[row, 1].Value = "FORNECEDOR CANDIDATOS TSE";
                                worksheet.Cells[row, 2].Value = tse.AnoEleicao;
                                worksheet.Cells[row, 3].Value = tse.Municipio;
                                worksheet.Cells[row, 4].Value = tse.UF;
                                worksheet.Cells[row, 5].Value = tse.Partido;
                                worksheet.Cells[row, 6].Value = tse.CNPJCPF;
                                worksheet.Cells[row, 7].Value = tse.Fornecedor;
                                worksheet.Cells[row, 8].Value = tse.DescricaoDespesas;
                                worksheet.Cells[row, 9].Value = tse.PartidoFornecedor;
                                worksheet.Cells[row, 10].Value = tse.ValorDespesas;

                                row++;
                            }
                        }
                    }
                }



                // Adicionar as seções de Dispensas e Licitações de forma similar...

                return package.GetAsByteArray();
            }
        }



        private byte[] GerarExcelCPF(List<ResultadoDTO> resultados)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Resultados CPF");

                int row = 1;

                // Adicionando a seção de Contratos
                if (resultados.Any(r => r.BuscaCompras?.Contrato != null && r.BuscaCompras.Contrato.Any()))
                {
                    worksheet.Cells[row, 1].Value = "CONTRATOS";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Contratos
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Orgão Gestor";
                    worksheet.Cells[row, 3].Value = "Status";
                    worksheet.Cells[row, 4].Value = "Protocolo";
                    worksheet.Cells[row, 7].Value = "Fornecedor";
                    worksheet.Cells[row, 8].Value = "CPF";
                    worksheet.Cells[row, 5].Value = "Valor Total Atual";
                    worksheet.Cells[row, 6].Value = "Valor Total Original";
                    worksheet.Cells[row, 9].Value = "Data Início Vigência";
                    worksheet.Cells[row, 10].Value = "Data Fim Vigência";
                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaCompras?.Contrato != null && resultado.BuscaCompras.Contrato.Any())
                        {
                            foreach (var contrato in resultado.BuscaCompras.Contrato)
                            {
                                worksheet.Cells[row, 1].Value = "Contrato";
                                worksheet.Cells[row, 2].Value = contrato.OrgaoGestor;
                                worksheet.Cells[row, 3].Value = contrato.StatusContrato;
                                worksheet.Cells[row, 4].Value = contrato.Protocolo;
                                worksheet.Cells[row, 7].Value = contrato.Fornecedor;
                                worksheet.Cells[row, 8].Value = contrato.CPF;
                                worksheet.Cells[row, 5].Value = contrato.VlrTotalAtual;
                                worksheet.Cells[row, 6].Value = contrato.VlrTotalOriginal;
                                worksheet.Cells[row, 9].Value = contrato.DTInicioVigencia?.ToString("dd/MM/yyyy") ?? "N/A";
                                worksheet.Cells[row, 10].Value = contrato.DTFimVigencia?.ToString("dd/MM/yyyy") ?? "N/A";

                                row++;
                            }
                        }
                    }
                }


                // Adicionando a seção de Dispensas
                if (resultados.Any(r => r.BuscaCompras?.Dispensa != null && r.BuscaCompras.Dispensa.Any()))
                {
                    worksheet.Cells[row, 1].Value = "DISPENSAS";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Dispensas
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Orgão";
                    worksheet.Cells[row, 3].Value = "Fornecedor";
                    worksheet.Cells[row, 4].Value = "CPF";
                    worksheet.Cells[row, 5].Value = "Protocolo";
                    worksheet.Cells[row, 6].Value = "Modalidade";
                    worksheet.Cells[row, 7].Value = "Valor Item";
                    worksheet.Cells[row, 8].Value = "Valor Dispensa";
                    worksheet.Cells[row, 9].Value = "Data Dispensa";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaCompras?.Dispensa != null && resultado.BuscaCompras.Dispensa.Any())
                        {
                            foreach (var contrato in resultado.BuscaCompras.Dispensa)
                            {
                                worksheet.Cells[row, 1].Value = "Dispensa";
                                worksheet.Cells[row, 2].Value = contrato.Orgao;
                                worksheet.Cells[row, 3].Value = contrato.Fornecedor;
                                worksheet.Cells[row, 4].Value = contrato.CPF;
                                worksheet.Cells[row, 5].Value = contrato.Protocolo;
                                worksheet.Cells[row, 6].Value = contrato.Modalidade;
                                worksheet.Cells[row, 7].Value = contrato.ValorItem;
                                worksheet.Cells[row, 8].Value = contrato.ValorDispensa;
                                worksheet.Cells[row, 9].Value = contrato.DataDispensa?.ToString("dd/MM/yyyy") ?? "N/A";


                                row++;
                            }
                        }
                    }
                }

                // Adicionando a seção de Licitações
                if (resultados.Any(r => r.BuscaCompras?.Licitacao != null && r.BuscaCompras.Licitacao.Any()))
                {
                    worksheet.Cells[row, 1].Value = "LICITAÇÕES";
                    worksheet.Cells[row, 1, row, 10].Merge = true;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    row++;

                    // Cabeçalho de Dispensas
                    worksheet.Cells[row, 1].Value = "Tipo de Registro";
                    worksheet.Cells[row, 2].Value = "Ano";
                    worksheet.Cells[row, 3].Value = "Orgão";
                    worksheet.Cells[row, 4].Value = "Fornecedor";
                    worksheet.Cells[row, 5].Value = "CPF";
                    worksheet.Cells[row, 6].Value = "Protocolo";
                    worksheet.Cells[row, 7].Value = "Valor Homologado";

                    row++;

                    foreach (var resultado in resultados)
                    {
                        if (resultado.BuscaCompras?.Licitacao != null && resultado.BuscaCompras.Licitacao.Any())
                        {
                            foreach (var contrato in resultado.BuscaCompras.Licitacao)
                            {
                                worksheet.Cells[row, 1].Value = "Dispensa";
                                worksheet.Cells[row, 2].Value = contrato.Ano;
                                worksheet.Cells[row, 3].Value = contrato.Orgao;
                                worksheet.Cells[row, 4].Value = contrato.Fornecedor;
                                worksheet.Cells[row, 5].Value = contrato.CPF;
                                worksheet.Cells[row, 6].Value = contrato.Protocolo;
                                worksheet.Cells[row, 7].Value = contrato.ValorHomologado;

                                row++;
                            }
                        }
                    }
                }



                // Adicionar as seções de Dispensas e Licitações de forma similar...

                return package.GetAsByteArray();
            }


        }




        [HttpPost]
        public async Task<FileStreamResult> GerarPdf([FromBody] PdfRequest request)
        {
            var dados = JsonConvert.DeserializeObject<ResultadoDTO>(request.DadosJson);

            var pdfFile = await _buscaService.GerarPDF(dados, request.Cnpj);

            return pdfFile;
        }

        // Classe auxiliar para receber os dados no JSON
        public class PdfRequest
        {
            public string DadosJson { get; set; }
            public string Cnpj { get; set; }
        }




    }
}


