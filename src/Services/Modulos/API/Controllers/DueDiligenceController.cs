using Domain.DueDiligence.Entidade;
using Domain.DueDiligence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/duediligence")]
    [Authorize(Roles = "Administrador")]

    public class DueDiligenceController : Controller
    {
        private readonly IDueRepository _dueRepository;
        private readonly IDueRepositoryRead _dueRepositoryRead;


        public DueDiligenceController(IDueRepository dueRepository, IDueRepositoryRead dueRepositoryRead)
        {
            _dueRepository = dueRepository;
            _dueRepositoryRead = dueRepositoryRead;
        }



        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5, string termo = null)
        {
            var pagedResult = await _dueRepositoryRead.Listar(pageNumber, pageSize, termo);
            return Ok(pagedResult);
        }


        [HttpGet("buscaId/{id}")]
        public async Task<Comissionado> BuscaId(Guid id)
        {
            return await _dueRepositoryRead.ObterId(id);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> BuscaPorCPF(string cpf)
        {
            var due = await _dueRepositoryRead.BuscarPorCPF(cpf);

            if (due == null || !due.Any())
            {
                return NotFound("Nenhum due encontrado para o CPF fornecido.");
            }

            return Ok(due);
        }

        [HttpPost("adicionar")]

        public async Task Add(Comissionado comissionado)
        {
            await _dueRepository.Adicionar(comissionado);
        }


        [HttpPut("alterar")]

        public async Task Update(Comissionado comissionado)
        {
            await _dueRepository.Atualizar(comissionado);
        }

        [HttpDelete("deletar/{id}")]
        public async Task RemoveInstauracao(Guid id)
        {
            var remover = await _dueRepositoryRead.ObterId(id);
            await _dueRepository.Deletar(remover);

        }

        [HttpGet("pesquisar-comissionado/{nroProtocolo}")]
        public async Task<IActionResult> PesquisarComissionado([FromRoute] string nroProtocolo)
        {
            if (string.IsNullOrWhiteSpace(nroProtocolo))
                return BadRequest("O número do protocolo deve ser informado.");

            var comissionado = await _dueRepositoryRead.ObterPorProtocolo(nroProtocolo);

            if (comissionado == null)
                return NotFound($"Nenhum comissionado encontrado com o número de protocolo {nroProtocolo}.");

            return Ok(comissionado);
        }




        [HttpGet("gerarpdf/{id}")]
        public async Task<IActionResult> GerarPdf(Guid id)
        {
            var dados = await _dueRepositoryRead.ObterId(id);

            var document = new PdfDocument();
            var page = document.AddPage();
            page.Width = XUnit.FromMillimeter(210); // Largura em milímetros
            page.Height = XUnit.FromMillimeter(297);
            var graphics = XGraphics.FromPdfPage(page);
            var textFormater = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);

            var logo = XImage.FromFile("D:\\projetos\\CGEODP\\src\\Services\\Due\\DueDiligence.API\\Controllers\\logo.png");
            var rodape = XImage.FromFile("D:\\projetos\\CGEODP\\src\\Services\\Due\\DueDiligence.API\\Controllers\\rodape.jpg");
            // Altura desejada para a imagem do logo
            double logoImageHeight = 50;

            // Calcular a proporção entre a altura original e a nova altura desejada
            double proportionl = logoImageHeight / logo.PixelHeight;

            // Calcular a largura redimensionada da imagem
            double logoImageWidth = logo.PixelWidth * proportionl;

            // Desenhar a imagem do logo com a largura e altura corretas
            double x = (page.Width - logoImageWidth) / 2;
            graphics.DrawImage(logo, x, 10, logoImageWidth, logoImageHeight);


            XFont font = new XFont("Arial", 12, XFontStyle.Regular);
            XFont titulo = new XFont("Arial", 14, XFontStyle.Bold);
            XFont negrito = new XFont("Arial", 10, XFontStyle.Bold);
            XFont normal = new XFont("Arial", 9, XFontStyle.Regular);
            XFont subscrito = new XFont("Arial", 8, XFontStyle.Bold);


            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
            textFormater.DrawString("Due Diligence para contratação de pessoal", titulo, XBrushes.DarkSlateGray, new XRect(0, 70, page.Width, page.Height));

            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            graphics.DrawRectangle(XPens.Gray, 20, 100, page.Width - 50, 40);
            textFormater.DrawString($"Nome:", negrito, XBrushes.Black, new XRect(25, 103, 5, 3));
            textFormater.DrawString($"{dados.Nome}", normal, XBrushes.Black, new XRect(60, 103, 300, 3));
            textFormater.DrawString($"CPF:", negrito, XBrushes.Black, new XRect(330, 103, 5, 3));
            textFormater.DrawString($"{dados.CPF}", normal, XBrushes.Black, new XRect(360, 103, 5, 11));
            textFormater.DrawString($"Órgão:", negrito, XBrushes.Black, new XRect(430, 103, 5, 3));
            textFormater.DrawString($"{dados.Orgao}", normal, XBrushes.Black, new XRect(470, 103, 5, 11));
            textFormater.DrawString($"Protocolo:", negrito, XBrushes.Black, new XRect(25, 123, 5, 3));
            textFormater.DrawString($"{dados.NroProtocolo}", normal, XBrushes.Black, new XRect(80, 123, 5, 11));
            textFormater.DrawString($"Data da Solicitação:", negrito, XBrushes.Black, new XRect(330, 123, 15, 3));
            textFormater.DrawString($"{dados.DataSolicitacao}", normal, XBrushes.Black, new XRect(360, 123, 15, 11));


            textFormater.DrawString("           Considerando o disposto no Decreto nº 8.038/2021 que estabelece a realização de due diligence na contratação de " +
                                    "pessoal para ocupação de cargo de provimento em comissão e de função de gestão pública da administração pública " +
                                    "direta, autárquica e fundacional. O Observatório da Despesa Públicado Estado do Paraná realizou consulta nas bases de " +
                                    "dados abaixo relacionadas e identificou os seguintes apontamentos:",
                                    normal, XBrushes.Black, new XRect(20, 150, page.Width - 50, 400));


            XStringFormat format = new XStringFormat();
            format.LineAlignment = XLineAlignment.Near;
            format.Alignment = XStringAlignment.Near;

            // Row elements
            int el1_width = 40;
            int el2_width = 420;
            int el3_width = 80;

            // page structure options
            double lineHeight = 17;
            int marginLeft = 20;
            int marginTop = 200;

            int el_height = 30;
            int rect_height = 14;

            int interLine_X_1 = 2;
            int interLine_X_2 = 2 * interLine_X_1;

            int offSetX_1 = el1_width;
            int offSetX_2 = el1_width + el2_width;

            XSolidBrush rect_style1 = new XSolidBrush(XColors.LightGray);
            XSolidBrush rect_style2 = new XSolidBrush(XColors.DarkGreen);



            graphics.DrawRectangle(rect_style2, marginLeft, marginTop, page.Width - 2.5 * marginLeft, rect_height);

            textFormater.DrawString("Base", negrito, XBrushes.White,
                          new XRect(marginLeft, marginTop, el1_width, el_height), format);

            textFormater.DrawString("Base de Dados", negrito, XBrushes.White,
                          new XRect(marginLeft + offSetX_1 + interLine_X_1, marginTop, el2_width, el_height), format);

            textFormater.DrawString("Situação", negrito, XBrushes.White,
                          new XRect(marginLeft + offSetX_2 + 2 * interLine_X_2, marginTop, el3_width, el_height), format);
            var i = 1;
            double dist_Y;
            double dist_Y2;
            var startup = false;
            XFont tabela;
            var flag = false;
            foreach (var property in typeof(Comissionado).GetProperties())
            {

                dist_Y = lineHeight * i;
                dist_Y2 = dist_Y - 2;
                var displayAttribute = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
                var columnNameAttribute = (ColumnAttribute)property.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault();
                if (property.Name == "CadFuncionarioPublico")
                {
                    startup = true;
                }
                if (displayAttribute != null && columnNameAttribute != null && startup)
                {
                    var value = property.GetValue(dados);

                    string displayValue = (value != null && value is bool && (bool)value) ? "consta" : "não consta";

                    if (property.Name == "CadInelegivel" || flag) { tabela = new XFont("Arial", 8, XFontStyle.Bold); flag = true; } else { tabela = new XFont("Arial", 8, XFontStyle.Regular); }

                    textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                    graphics.DrawRectangle(rect_style1, marginLeft, dist_Y2 + marginTop, el1_width, rect_height);
                    textFormater.DrawString($"{i}", normal, XBrushes.Black,
                                  new XRect(marginLeft, dist_Y + marginTop, el1_width, el_height), format);

                    //ELEMENT 2 - BIG 380
                    textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    graphics.DrawRectangle(rect_style1, marginLeft + offSetX_1 + interLine_X_1, dist_Y2 + marginTop, el2_width, rect_height);
                    textFormater.DrawString(
                       $"{displayAttribute.Name}",
                        tabela,
                        XBrushes.Black,
                        new XRect(marginLeft + offSetX_1 + interLine_X_1, dist_Y + marginTop, el2_width, el_height),
                        format);


                    //ELEMENT 3 - SMALL 80
                    textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                    graphics.DrawRectangle(rect_style1, marginLeft + offSetX_2 + interLine_X_2, dist_Y2 + marginTop, el3_width, rect_height);
                    textFormater.DrawString(
                        $"{displayValue}",
                        tabela,
                        XBrushes.Black,
                        new XRect(marginLeft + offSetX_2 + 2 * interLine_X_2, dist_Y + marginTop, el3_width, el_height),
                        format);

                    i++;
                }

            }

            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            textFormater.DrawString("           As consultas são provenientes de bancos de dados públicos e privados, com informações e/ou períodos de atualizações distintos, " +
                "podendo estar desatualizados no momento da consulta.\r\n           Deve-se considerar ainda que as informações constantes no presente protocolo e despacho são informações " +
                "de dados pessoais e portanto dados sensíveis, dessa forma sendo prudente a caracterização deste e dos documentos que o compõem como de caráter sigilosos (LGPD)²." +
                "\r\n           Encaminhe-se ao gabinete da CGE para elaboração do parecer opinativo, conforme estabelece o art. 4º do Decreto Estadual nº 8.038/2021 e o inciso II do " +
                "art. 2º da Resolução Conjunta CGE/CC/SEAP nº 01/2021.",
                                    normal, XBrushes.Black, new XRect(20, 660, page.Width - 50, 400));

            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
            textFormater.DrawString("Observatório da Despesa Pública", titulo, XBrushes.DarkSlateGray, new XRect(0, 750, page.Width, page.Height));
            graphics.DrawLine(XPens.DarkGray, 20, 770, page.Width - 50, 770);


            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            textFormater.DrawString("1 - Lei 14.133/2021, Art. 14 Inciso IV | 2 - Lei Geral de Proteção de Dados (LGPD) – Lei nº 13.709/2018.",
                                    subscrito, XBrushes.Black, new XRect(20, 772, page.Width - 50, 400));

            // Altura desejada para a imagem no rodapé
            double footerImageHeight = 50;

            // Calcular a proporção entre a altura original e a nova altura desejada
            double proportion = footerImageHeight / rodape.PixelHeight;

            // Calcular a largura redimensionada da imagem
            double footerImageWidth = rodape.PixelWidth * proportion;

            // Desenhar a imagem no rodapé com a largura e altura corretas
            graphics.DrawImage(rodape, page.Width - footerImageWidth, page.Height - footerImageHeight + 10, footerImageWidth, footerImageHeight);


            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;


            return new FileStreamResult(stream, "application/pdf") { FileDownloadName = "elemento.pdf" };
        }


        [HttpPost("gerarpdf")]
        public async Task<IActionResult> GerarPdf(Comissionado comissionado)
        {
            var dados = comissionado;

            var document = new PdfDocument();
            var page = document.AddPage();
            page.Width = XUnit.FromMillimeter(210); // Largura em milímetros
            page.Height = XUnit.FromMillimeter(297);
            var graphics = XGraphics.FromPdfPage(page);
            var textFormater = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);

            var logo = XImage.FromFile("D:\\projetos\\CGEODP\\src\\Services\\Due\\DueDiligence.API\\Controllers\\logo.png");
            var rodape = XImage.FromFile("D:\\projetos\\CGEODP\\src\\Services\\Due\\DueDiligence.API\\Controllers\\rodape.jpg");
            // Altura desejada para a imagem do logo
            double logoImageHeight = 50;

            // Calcular a proporção entre a altura original e a nova altura desejada
            double proportionl = logoImageHeight / logo.PixelHeight;

            // Calcular a largura redimensionada da imagem
            double logoImageWidth = logo.PixelWidth * proportionl;

            // Desenhar a imagem do logo com a largura e altura corretas
            double x = (page.Width - logoImageWidth) / 2;
            graphics.DrawImage(logo, x, 10, logoImageWidth, logoImageHeight);


            XFont font = new XFont("Arial", 12, XFontStyle.Regular);
            XFont titulo = new XFont("Arial", 14, XFontStyle.Bold);
            XFont negrito = new XFont("Arial", 10, XFontStyle.Bold);
            XFont normal = new XFont("Arial", 9, XFontStyle.Regular);
            XFont subscrito = new XFont("Arial", 8, XFontStyle.Bold);


            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
            textFormater.DrawString("Due Diligence para contratação de pessoal", titulo, XBrushes.DarkSlateGray, new XRect(0, 70, page.Width, page.Height));

            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            graphics.DrawRectangle(XPens.Gray, 20, 100, page.Width - 50, 40);
            textFormater.DrawString($"Nome:", negrito, XBrushes.Black, new XRect(25, 103, 5, 3));
            textFormater.DrawString($"{dados.Nome}", normal, XBrushes.Black, new XRect(60, 103, 300, 3));
            textFormater.DrawString($"CPF:", negrito, XBrushes.Black, new XRect(330, 103, 5, 3));
            textFormater.DrawString($"{dados.CPF}", normal, XBrushes.Black, new XRect(360, 103, 5, 11));
            textFormater.DrawString($"Órgão:", negrito, XBrushes.Black, new XRect(430, 103, 5, 3));
            textFormater.DrawString($"{dados.Orgao}", normal, XBrushes.Black, new XRect(470, 103, 5, 11));
            textFormater.DrawString($"Protocolo:", negrito, XBrushes.Black, new XRect(25, 123, 5, 3));
            textFormater.DrawString($"{dados.NroProtocolo}", normal, XBrushes.Black, new XRect(80, 123, 5, 11));
            textFormater.DrawString($"Data da Solicitação:", negrito, XBrushes.Black, new XRect(330, 123, 15, 3));
            textFormater.DrawString($"{dados.DataSolicitacao}", normal, XBrushes.Black, new XRect(360, 123, 15, 11));


            textFormater.DrawString("           Considerando o disposto no Decreto nº 8.038/2021 que estabelece a realização de due diligence na contratação de " +
                                    "pessoal para ocupação de cargo de provimento em comissão e de função de gestão pública da administração pública " +
                                    "direta, autárquica e fundacional. O Observatório da Despesa Públicado Estado do Paraná realizou consulta nas bases de " +
                                    "dados abaixo relacionadas e identificou os seguintes apontamentos:",
                                    normal, XBrushes.Black, new XRect(20, 150, page.Width - 50, 400));


            XStringFormat format = new XStringFormat();
            format.LineAlignment = XLineAlignment.Near;
            format.Alignment = XStringAlignment.Near;

            // Row elements
            int el1_width = 40;
            int el2_width = 420;
            int el3_width = 80;

            // page structure options
            double lineHeight = 18;
            int marginLeft = 20;
            int marginTop = 200;

            int el_height = 30;
            int rect_height = 14;

            int interLine_X_1 = 2;
            int interLine_X_2 = 2 * interLine_X_1;

            int offSetX_1 = el1_width;
            int offSetX_2 = el1_width + el2_width;

            XSolidBrush rect_style1 = new XSolidBrush(XColors.LightGray);
            XSolidBrush rect_style2 = new XSolidBrush(XColors.DarkGreen);



            graphics.DrawRectangle(rect_style2, marginLeft, marginTop, page.Width - 2.5 * marginLeft, rect_height);

            textFormater.DrawString("Base", negrito, XBrushes.White,
                          new XRect(marginLeft, marginTop, el1_width, el_height), format);

            textFormater.DrawString("Base de Dados", negrito, XBrushes.White,
                          new XRect(marginLeft + offSetX_1 + interLine_X_1, marginTop, el2_width, el_height), format);

            textFormater.DrawString("Situação", negrito, XBrushes.White,
                          new XRect(marginLeft + offSetX_2 + 2 * interLine_X_2, marginTop, el3_width, el_height), format);
            var i = 1;
            double dist_Y;
            double dist_Y2;
            var startup = false;
            XFont tabela;
            var flag = false;
            foreach (var property in typeof(Comissionado).GetProperties())
            {

                dist_Y = lineHeight * i;
                dist_Y2 = dist_Y - 2;
                var displayAttribute = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
                var columnNameAttribute = (ColumnAttribute)property.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault();
                if (property.Name == "CadEmpresario")
                {
                    startup = true;
                }
                if (displayAttribute != null && columnNameAttribute != null && startup)
                {
                    var value = property.GetValue(dados);

                    string displayValue = (value != null && value is bool && (bool)value) ? "consta" : "não consta";

                    if (property.Name == "CadEmpresaContrato" || flag) { tabela = new XFont("Arial", 8, XFontStyle.Bold); flag = true; } else { tabela = new XFont("Arial", 8, XFontStyle.Regular); }

                    textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                    graphics.DrawRectangle(rect_style1, marginLeft, dist_Y2 + marginTop, el1_width, rect_height);
                    textFormater.DrawString($"{i}", normal, XBrushes.Black,
                                  new XRect(marginLeft, dist_Y + marginTop, el1_width, el_height), format);

                    //ELEMENT 2 - BIG 380
                    textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
                    graphics.DrawRectangle(rect_style1, marginLeft + offSetX_1 + interLine_X_1, dist_Y2 + marginTop, el2_width, rect_height);
                    textFormater.DrawString(
                       $"{displayAttribute.Name}",
                        tabela,
                        XBrushes.Black,
                        new XRect(marginLeft + offSetX_1 + interLine_X_1, dist_Y + marginTop, el2_width, el_height),
                        format);


                    //ELEMENT 3 - SMALL 80
                    textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                    graphics.DrawRectangle(rect_style1, marginLeft + offSetX_2 + interLine_X_2, dist_Y2 + marginTop, el3_width, rect_height);
                    textFormater.DrawString(
                        $"{displayValue}",
                        tabela,
                        XBrushes.Black,
                        new XRect(marginLeft + offSetX_2 + 2 * interLine_X_2, dist_Y + marginTop, el3_width, el_height),
                        format);

                    i++;
                }

            }

            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            textFormater.DrawString("           As consultas são provenientes de bancos de dados públicos e privados, com informações e/ou períodos de atualizações distintos, " +
                "podendo estar desatualizados no momento da consulta.\r\n           Deve-se considerar ainda que as informações constantes no presente protocolo e despacho são informações " +
                "de dados pessoais e portanto dados sensíveis, dessa forma sendo prudente a caracterização deste e dos documentos que o compõem como de caráter sigilosos (LGPD)²." +
                "\r\n           Encaminhe-se ao gabinete da CGE para elaboração do parecer opinativo, conforme estabelece o art. 4º do Decreto Estadual nº 8.038/2021 e o inciso II do " +
                "art. 2º da Resolução Conjunta CGE/CC/SEAP nº 01/2021.",
                                    normal, XBrushes.Black, new XRect(20, 660, page.Width - 50, 400));

            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
            textFormater.DrawString("Observatório da Despesa Pública", titulo, XBrushes.DarkSlateGray, new XRect(0, 750, page.Width, page.Height));
            graphics.DrawLine(XPens.DarkGray, 20, 770, page.Width - 50, 770);


            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            textFormater.DrawString("1 - Lei 14.133/2021, Art. 14 Inciso IV | 2 - Lei Geral de Proteção de Dados (LGPD) – Lei nº 13.709/2018.",
                                    subscrito, XBrushes.Black, new XRect(20, 772, page.Width - 50, 400));

            // Altura desejada para a imagem no rodapé
            double footerImageHeight = 50;

            // Calcular a proporção entre a altura original e a nova altura desejada
            double proportion = footerImageHeight / rodape.PixelHeight;

            // Calcular a largura redimensionada da imagem
            double footerImageWidth = rodape.PixelWidth * proportion;

            // Desenhar a imagem no rodapé com a largura e altura corretas
            graphics.DrawImage(rodape, page.Width - footerImageWidth, page.Height - footerImageHeight + 10, footerImageWidth, footerImageHeight);


            MemoryStream stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;


            return new FileStreamResult(stream, "application/pdf") { FileDownloadName = "elemento.pdf" };
        }
    }

}
