using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Consultas.ResultadoConsulta;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas
{


    public class BuscaService : IBuscaService
    {
        private readonly IComprasServicos _comprasServicos;
        private readonly IDividasServicos _dividasServicos;
        private readonly IFazendaServicos _fazendaServicos;
        private readonly IGovernoEstadualServicos _governoEstadualServicos;
        private readonly IGovernoFederalServicos _governoFederalServicos;
        private readonly ITCUServicos _tcuServicos;
        private readonly ITCEServicos _tceServicos;
        private readonly ITSEServicos _tseServicos;
        private readonly IInternosServicos _internoServicos;

        public BuscaService(IComprasServicos comprasServicos,
                            IDividasServicos dividasServicos,
                            IFazendaServicos fazendaServicos,
                            IGovernoEstadualServicos governoEstadualServicos,
                            IGovernoFederalServicos governoFederalServicos,
                            ITCUServicos tcuServicos,
                            ITCEServicos tceServicos,
                            ITSEServicos tseServicos,
                            IInternosServicos internoServicos)
        {
            _comprasServicos = comprasServicos;
            _dividasServicos = dividasServicos;
            _fazendaServicos = fazendaServicos;
            _governoEstadualServicos = governoEstadualServicos;
            _governoFederalServicos = governoFederalServicos;
            _tcuServicos = tcuServicos;
            _tceServicos = tceServicos;
            _tseServicos = tseServicos;
            _internoServicos = internoServicos;
        }

        // Método utilitário para executar ações com tratamento de erro
        private async Task<T> ExecutarComTratamentoDeErro<T>(Func<Task<T>> acao)
        {
            try
            {
                return await acao();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar ação: {ex.Message}");
                return default;
            }
        }

        // Métodos de busca individual
        public async Task<ResultadoDTO> BuscarCNPJ(string cnpj)
        {
            var resultado = new ResultadoDTO
            {

                BuscaCompras = await ExecutarComTratamentoDeErro(() => _comprasServicos.BuscarCNPJ(cnpj)),
                BuscaDividas = await ExecutarComTratamentoDeErro(() => _dividasServicos.BuscarCNPJ(cnpj)),
                BuscaFazenda = await ExecutarComTratamentoDeErro(() => _fazendaServicos.BuscarCNPJ(cnpj)),
                BuscaEstadual = await ExecutarComTratamentoDeErro(() => _governoEstadualServicos.BuscarCNPJ(cnpj)),
                BuscaFederal = await ExecutarComTratamentoDeErro(() => _governoFederalServicos.BuscarCNPJ(cnpj)),
                BuscaTCU = await ExecutarComTratamentoDeErro(() => _tcuServicos.BuscarCNPJ(cnpj)),
                BuscaTCE = await ExecutarComTratamentoDeErro(() => _tceServicos.BuscarCNPJ(cnpj)),
                BuscaTSE = await ExecutarComTratamentoDeErro(() => _tseServicos.BuscarCNPJ(cnpj)),
                //BuscaInterno = await ExecutarComTratamentoDeErro(() => _internoServicos.BuscarCNPJ(cnpj)),

            };



            return resultado;
        }

        public async Task<ResultadoDTO> BuscarCPF(string cpf)
        {
            var resultado = new ResultadoDTO
            {

                BuscaCompras = await ExecutarComTratamentoDeErro(() => _comprasServicos.BuscarCPF(cpf)),
                BuscaDividas = await ExecutarComTratamentoDeErro(() => _dividasServicos.BuscarCPF(cpf)),
                BuscaFazenda = await ExecutarComTratamentoDeErro(() => _fazendaServicos.BuscarCPF(cpf)),
                BuscaEstadual = await ExecutarComTratamentoDeErro(() => _governoEstadualServicos.BuscarCPF(cpf)),
                BuscaFederal = await ExecutarComTratamentoDeErro(() => _governoFederalServicos.BuscarCPF(cpf)),
                BuscaTCU = await ExecutarComTratamentoDeErro(() => _tcuServicos.BuscarCPF(cpf)),
                BuscaTCE = await ExecutarComTratamentoDeErro(() => _tceServicos.BuscarCPF(cpf)),
                BuscaTSE = await ExecutarComTratamentoDeErro(() => _tseServicos.BuscarCPF(cpf)),
                //BuscaInterno = await ExecutarComTratamentoDeErro(() => _internoServicos.BuscarCPF(cpf))
            };

            return resultado;
        }




        public async Task<ResultadoDTO> BuscarCNPJPorTabelasSelecionadas(string cnpj, List<string> tabelasSelecionadas)
        {
            var resultado = new ResultadoDTO();

            if (tabelasSelecionadas.Contains("compras"))
                resultado.BuscaCompras = await _comprasServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("dividas"))
                resultado.BuscaDividas = await _dividasServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("fazenda"))
                resultado.BuscaFazenda = await _fazendaServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("estadual"))
                resultado.BuscaEstadual = await _governoEstadualServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("federal"))
                resultado.BuscaFederal = await _governoFederalServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("tce"))
                resultado.BuscaTCE = await _tceServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("tcu"))
                resultado.BuscaTCU = await _tcuServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("tse"))
                resultado.BuscaTSE = await _tseServicos.BuscarCNPJ(cnpj);



            return resultado;
        }


        public async Task<ResultadoDTO> BuscarCPFPorTabelasSelecionadas(string cpf, List<string> tabelasSelecionadas)
        {
            var resultado = new ResultadoDTO();

            if (tabelasSelecionadas.Contains("compras"))
                resultado.BuscaCompras = await _comprasServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("dividas"))
                resultado.BuscaDividas = await _dividasServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("fazenda"))
                resultado.BuscaFazenda = await _fazendaServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("estadual"))
                resultado.BuscaEstadual = await _governoEstadualServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("federal"))
                resultado.BuscaFederal = await _governoFederalServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("tce"))
                resultado.BuscaTCE = await _tceServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("tcu"))
                resultado.BuscaTCU = await _tcuServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("tse"))
                resultado.BuscaTSE = await _tseServicos.BuscarCPF(cpf);

            //if (tabelasSelecionadas.Contains("interno"))
            //    resultado.BuscaInterno = await _internoServicos.BuscarCPF(cpf);





            return resultado;
        }


        public async Task<List<ResultadoDTO>> BuscarEmLoteAsync(List<string> identificadores, bool isCNPJ, List<string> tabelasSelecionadas)
        {
            var resultados = new List<ResultadoDTO>();

            foreach (var id in identificadores)
            {
                ResultadoDTO resultado;

                if (isCNPJ)
                {
                    resultado = await BuscarCNPJPorTabelasSelecionadas(id, tabelasSelecionadas);
                }
                else
                {
                    resultado = await BuscarCPFPorTabelasSelecionadas(id, tabelasSelecionadas);
                }

                resultados.Add(resultado);
            }

            return resultados;
        }

        public Task<FileStreamResult> GerarPDF(ResultadoDTO dados, string cnpj)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            page.Width = XUnit.FromMillimeter(210);
            page.Height = XUnit.FromMillimeter(297);
            var graphics = XGraphics.FromPdfPage(page);
            var textFormater = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);

            // Adicionar logo
            var logo = XImage.FromFile("D:\\projetos\\CGEODP\\src\\Services\\Due\\DueDiligence.API\\Controllers\\logo.png");
            double logoImageHeight = 50;
            double proportionl = logoImageHeight / logo.PixelHeight;
            double logoImageWidth = logo.PixelWidth * proportionl;
            double x = (page.Width - logoImageWidth) / 2;
            graphics.DrawImage(logo, x, 10, logoImageWidth, logoImageHeight);

            // Adicionar título
            XFont titulo = new XFont("Arial", 14, XFontStyle.Bold);
            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
            textFormater.DrawString("Resultado da consulta do CNPJ às bases de dados", titulo, XBrushes.DarkSlateGray, new XRect(0, 70, page.Width, page.Height));

            // Adicionar CNPJ e data
            XFont negrito = new XFont("Arial", 10, XFontStyle.Bold);
            XFont normal = new XFont("Arial", 9, XFontStyle.Regular);
            graphics.DrawRectangle(XPens.Gray, 20, 100, page.Width - 50, 40);
            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            textFormater.DrawString($"CNPJ:", negrito, XBrushes.Black, new XRect(25, 103, 5, 3));
            textFormater.DrawString($"{cnpj}", normal, XBrushes.Black, new XRect(60, 103, 300, 3));
            textFormater.DrawString($"Data da pesquisa:", negrito, XBrushes.Black, new XRect(330, 103, 5, 3));
            textFormater.DrawString($"{DateTime.Today:dd/MM/yyyy}", normal, XBrushes.Black, new XRect(360, 103, 5, 11));

            // Criar matriz com bases de dados
            double startY = 150;
            double rowHeight = 20;
            double col1Width = 150;
            double col2Width = 100;
            double col3Width = 50;

            var bases = new Dictionary<string, bool>
    {
        { "Contratos", dados.BuscaCompras?.Contrato?.Any() == true },
        { "Dispensas", dados.BuscaCompras?.Dispensa?.Any() == true },
        { "Licitações", dados.BuscaCompras?.Licitacao?.Any() == true },
        { "Dívida FGTS", dados.BuscaDividas?.DividaFGTS?.Any() == true },
        { "Dívida Previdenciária", dados.BuscaDividas?.DividaPrevidenciaria?.Any() == true },
        { "Dívida Não Previdenciária", dados.BuscaDividas?.DividaNaoPrevidenciaria?.Any() == true },
        { "Jucepar", dados.BuscaFazenda?.Jucepar?.Any() == true },
        { "NF Eletrônica Federal", dados.BuscaFazenda?.NFEletronicaFederal?.Any() == true },
        { "NF Eletrônica Estadual", dados.BuscaFazenda?.NFEletronica?.Any() == true },
    };

            foreach (var entry in bases)
            {
                graphics.DrawRectangle(XPens.Black, 20, startY, col1Width, rowHeight);
                textFormater.DrawString(entry.Key, normal, XBrushes.Black, new XRect(25, startY + 5, col1Width, rowHeight));

                graphics.DrawRectangle(XPens.Black, 20 + col1Width, startY, col2Width, rowHeight);
                textFormater.DrawString(entry.Value ? "X" : "", negrito, XBrushes.Black, new XRect(25 + col1Width, startY + 5, col2Width, rowHeight));

                startY += rowHeight;
            }

            // Adicionar rodapé
            var rodape = XImage.FromFile("D:\\projetos\\CGEODP\\src\\Services\\Due\\DueDiligence.API\\Controllers\\rodape.jpg");
            double footerImageHeight = 50;
            double proportion = footerImageHeight / rodape.PixelHeight;
            double footerImageWidth = rodape.PixelWidth * proportion;
            graphics.DrawImage(rodape, page.Width - footerImageWidth, page.Height - footerImageHeight + 10, footerImageWidth, footerImageHeight);

            var stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;
            return Task.FromResult(new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = $"Relatorio_{cnpj}.pdf"
            });
        }








    }



}
