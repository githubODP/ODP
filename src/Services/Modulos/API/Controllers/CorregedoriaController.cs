
using CsvHelper;
using Domain.Corregedoria.Entidade;
using Domain.Corregedoria.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{


    [ApiController]
    [Route("api/corregedoria")]
    [Authorize]
    public class CorregedoriaController : Controller
    {
        private readonly IInstauracaoRepository _instauracaoRepository;
        private readonly IInstauracaoRepositoryRead _instauracaoRepositoryRead;


        public CorregedoriaController(IInstauracaoRepository instauracaoRepository, IInstauracaoRepositoryRead instauracaoRepositoryRead)
        {
            _instauracaoRepository = instauracaoRepository;
            _instauracaoRepositoryRead = instauracaoRepositoryRead;
        }





        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _instauracaoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }




        [HttpGet("buscaId/{id}")]
        public async Task<Instauracao> BuscaId(Guid id)
        {
            return await _instauracaoRepositoryRead.ObterId(id);
        }


        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<Instauracao> SearchCNPJ(string cnpj)
        {
            return await _instauracaoRepositoryRead.BuscarPorCNPJ(cnpj);
        }

        [HttpGet("consultacpf/{cpf}")]
        public async Task<Instauracao> SearchCPF(string cpf)
        {
            return await _instauracaoRepositoryRead.BuscarPorCPF(cpf);
        }

        [HttpPost("adicionar")]

        public async Task Add(Instauracao instauracao)
        {
            await _instauracaoRepository.Adicionar(instauracao);
        }


        [HttpPut("alterar")]

        public async Task Update(Instauracao instauracao)
        {
            await _instauracaoRepository.Atualizar(instauracao);
        }

        [HttpDelete("deletar/{id}")]
        public async Task RemoveInstauracao(Guid id)
        {
            var remover = await _instauracaoRepositoryRead.ObterId(id);
            await _instauracaoRepository.Deletar(remover);

        }



        [HttpGet("gerarpdf/{id}")]
        public async Task<IActionResult> GerarPdf(Guid id)
        {
            //var dados = await _instauracaoRepository.ObterId(id);

            var document = new PdfDocument();
            var page = document.AddPage();
            page.Width = XUnit.FromMillimeter(210); // Largura em milímetros
            page.Height = XUnit.FromMillimeter(297);
            var graphics = XGraphics.FromPdfPage(page);
            var textFormater = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);

            var logo = XImage.FromFile("C:\\ProjetosC#ODP\\CGEODP\\src\\Services\\Due\\DueDiligence.API\\Controllers\\logo.png");
            var rodape = XImage.FromFile("C:\\ProjetosC#ODP\\CGEODP\\src\\Services\\Due\\DueDiligence.API\\Controllers\\rodape.jpg");
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
            textFormater.DrawString("ESPAÇO DESTINADO A INFORMAÇOES E/OU GERAÇAO DE CERTIDAO", titulo, XBrushes.DarkSlateGray, new XRect(0, 70, page.Width, page.Height));

            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            graphics.DrawRectangle(XPens.Gray, 20, 100, page.Width - 50, 40);
            textFormater.DrawString($"Nome:", negrito, XBrushes.Black, new XRect(25, 103, 5, 3));
            //textFormater.DrawString($"{dados.Nome}", normal, XBrushes.Black, new XRect(60, 103, 100, 3));
            textFormater.DrawString($"CNPJ/CPF:", negrito, XBrushes.Black, new XRect(330, 103, 5, 3));
            //textFormater.DrawString($"{dados.CPF}", normal, XBrushes.Black, new XRect(360, 103, 5, 11));
            textFormater.DrawString($"Órgão:", negrito, XBrushes.Black, new XRect(430, 103, 5, 3));
            //textFormater.DrawString($"{dados.Orgao}", normal, XBrushes.Black, new XRect(470, 103, 5, 11));
            textFormater.DrawString($"Protocolo:", negrito, XBrushes.Black, new XRect(25, 123, 5, 3));
            //textFormater.DrawString($"{dados.NroProtocolo}", normal, XBrushes.Black, new XRect(80, 123, 5, 11));
            textFormater.DrawString($"Data da Emissao:", negrito, XBrushes.Black, new XRect(330, 123, 15, 3));
            //textFormater.DrawString($"{dados.DataSolicitacao}", normal, XBrushes.Black, new XRect(360, 123, 15, 11));


            textFormater.DrawString("           XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX " +
                                    "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX " +
                                    "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX " +
                                    "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                                    normal, XBrushes.Black, new XRect(20, 150, page.Width - 50, 400));


            XStringFormat format = new XStringFormat();
            format.LineAlignment = XLineAlignment.Near;
            format.Alignment = XStringAlignment.Near;

            // Row elements
            int el1_width = 80;
            int el2_width = 380;

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
            XSolidBrush rect_style3 = new XSolidBrush(XColors.Red);


            graphics.DrawRectangle(rect_style2, marginLeft, marginTop, page.Width - 2.5 * marginLeft, rect_height);

            textFormater.DrawString("Inicio ", negrito, XBrushes.White,
                          new XRect(marginLeft, marginTop, el1_width, el_height), format);

            textFormater.DrawString("andamento", negrito, XBrushes.White,
                          new XRect(marginLeft + offSetX_1 + interLine_X_1, marginTop, el2_width, el_height), format);

            textFormater.DrawString("Situação", negrito, XBrushes.White,
                          new XRect(marginLeft + offSetX_2 + 2 * interLine_X_2, marginTop, el1_width, el_height), format);
            var i = 1;
            double dist_Y = 0;
            double dist_Y2 = 0;
            var startup = false;
            XFont tabela;
            var flag = false;
            //foreach (var property in typeof(Corregedoria).GetProperties())
            //{

            //    dist_Y = lineHeight * i;
            //    dist_Y2 = dist_Y - 2;
            //    var displayAttribute = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
            //    var columnNameAttribute = (ColumnAttribute)property.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault();
            //    if (property.Name == "CadEmpresario")
            //    {
            //        startup = true;
            //    }
            //    if (displayAttribute != null && columnNameAttribute != null && startup)
            //    {

            //        // Use displayAttribute.Name para o nome a ser exibido na tabela
            //        // Use columnNameAttribute.Name para o nome da coluna no banco de dados

            //        // Aqui você pode usar o nome da propriedade para acessar o valor do objeto Comissionado
            //       /* var value = property.GetValue(dados);*/ //
            //                                              // supondo que comissionado seja uma instância de Comissionado
            //        //string displayValue = (bool)value ? "consta" : "não consta";
            //        //if (property.Name == "CadEmpresaContrato" || flag) { tabela = new XFont("Arial", 8, XFontStyle.Bold); flag = true; } else { tabela = new XFont("Arial", 8, XFontStyle.Regular); }

            //        textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
            //        graphics.DrawRectangle(rect_style1, marginLeft, dist_Y2 + marginTop, el1_width, rect_height);
            //        textFormater.DrawString($"{i}", normal, XBrushes.Black,
            //                      new XRect(marginLeft, dist_Y + marginTop, el1_width, el_height), format);

            //        //ELEMENT 2 - BIG 380
            //        textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            //        graphics.DrawRectangle(rect_style1, marginLeft + offSetX_1 + interLine_X_1, dist_Y2 + marginTop, el2_width, rect_height);
            //        textFormater.DrawString(
            //           $"{displayAttribute.Name}",
            //            tabela,
            //            XBrushes.Black,
            //            new XRect(marginLeft + offSetX_1 + interLine_X_1, dist_Y + marginTop, el2_width, el_height),
            //            format);


            //        //ELEMENT 3 - SMALL 80
            //        textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
            //        graphics.DrawRectangle(rect_style1, marginLeft + offSetX_2 + interLine_X_2, dist_Y2 + marginTop, el1_width, rect_height);
            //        textFormater.DrawString(
            //            $"{displayValue}",
            //            tabela,
            //            XBrushes.Black,
            //            new XRect(marginLeft + offSetX_2 + 2 * interLine_X_2, dist_Y + marginTop, el1_width, el_height),
            //            format);

            //        i++;
            //    }

            //}

            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            textFormater.DrawString("espaço destinado a informaçoes pertinentes e/ou geraççao de certidao ",
                                    normal, XBrushes.Black, new XRect(20, 660, page.Width - 50, 400));

            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
            textFormater.DrawString("Corregedoria - CCOR ", titulo, XBrushes.DarkSlateGray, new XRect(0, 750, page.Width, page.Height));
            graphics.DrawLine(XPens.DarkGray, 20, 770, page.Width - 50, 770);


            textFormater.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Left;
            textFormater.DrawString("1 - Lei (informativo de lei ) | 2 - Lei Geral de Proteção de Dados (LGPD) – Lei nº 13.709/2018.",
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


            return File(stream.ToArray(), "application/pdf", "elemento.pdf");
        }

        [HttpPost("uploadcsv")]
        public async Task<IActionResult> UploadCsv(IFormFile file, [FromQuery] int ps = 10, [FromQuery] int page = 1)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Por favor, selecione um arquivo CSV válido.");
            }

            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var instauracoesTemp = await LerInstauracaoTempCsvAsync(filePath);


            return Ok(instauracoesTemp);
        }

        private async Task<IEnumerable<Instauracao>> LerInstauracaoTempCsvAsync(string filePath)
        {
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.GetCultureInfo("pt-BR"))
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                BadDataFound = null,
                MissingFieldFound = null,
                HeaderValidated = null
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                var instauracoesTemp = csv.GetRecords<Instauracao>().ToList();
                return instauracoesTemp;
            }
        }


    }
}
