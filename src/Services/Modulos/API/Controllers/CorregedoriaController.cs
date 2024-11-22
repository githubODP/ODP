
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Corregedoria.Entidade;
using Domain.Corregedoria.Enum;
using Domain.Corregedoria.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API.Controllers
{


    [ApiController]
    [Route("api/corregedoria")]
    [Authorize(Roles = "Administrador, Operador")]
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
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 8)
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


        [HttpPost("uploadcsv")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Por favor, selecione um arquivo CSV válido.");
            }

            var instauracoes = new List<Instauracao>();
            var linhasComErro = new List<string>();

            try
            {
                // Salva o arquivo CSV em um local temporário
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Configuração do CsvReader
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, // Considera a linha de cabeçalho
                    Delimiter = ";", // Delimitador do arquivo CSV
                    TrimOptions = TrimOptions.Trim, // Remove espaços desnecessários
                    BadDataFound = args =>
                    {
                        // Captura dados ruins (linhas mal formatadas)
                        linhasComErro.Add($"Linha {args.Context.Parser.RawRow}: Dado ruim encontrado: {args.Field}");
                    },
                    MissingFieldFound = null, // Ignora campos ausentes
                    Encoding = Encoding.UTF8 // Garante a leitura correta de caracteres especiais
                };

                // Lê o arquivo CSV com encoding UTF-8
                using (var reader = new StreamReader(filePath, Encoding.UTF8))
                using (var csv = new CsvReader(reader, config))
                {
                    int rowNumber = 1; // Para rastrear o número da linha
                    csv.Context.RegisterClassMap<InstauracaoMap>(); // Mapeamento da classe

                    while (await csv.ReadAsync())
                    {
                        try
                        {
                            var instauracao = csv.GetRecord<Instauracao>();
                            instauracoes.Add(instauracao);
                        }
                        catch (Exception ex)
                        {
                            // Adiciona detalhes do erro
                            linhasComErro.Add($"Erro na linha {rowNumber}: {ex.Message}");
                        }

                        rowNumber++;
                    }
                }

                // Insere os registros no banco de dados
                foreach (var instauracao in instauracoes)
                {
                    await _instauracaoRepository.Adicionar(instauracao);
                }

                // Retorna sucesso com o número de registros processados e erros encontrados
                return Ok(new
                {
                    Message = "Arquivo processado com sucesso.",
                    TotalRegistros = instauracoes.Count,
                    LinhasComErro = linhasComErro
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
            }
        }

        // Classe de mapeamento para corrigir problemas de tipo
        public sealed class InstauracaoMap : ClassMap<Instauracao>
        {
            public InstauracaoMap()
            {
                Map(m => m.Ano).Index(0).TypeConverterOption.NullValues("0");
                Map(m => m.CNPJCPF).Index(1);
                Map(m => m.RG).Index(2);
                Map(m => m.Orgao).Index(3);
                Map(m => m.Procedimento).Index(4);
                Map(m => m.Protocolo).Index(5);
                Map(m => m.Objeto).Index(6);
                Map(m => m.AtoNormativo).Index(7);
                Map(m => m.DataPublicacao).Index(8).TypeConverterOption.Format("dd/MM/yyyy");
                Map(m => m.NumeroDIOE).Index(9);
                Map(m => m.AtoNormativoDecisao).Index(10);
                Map(m => m.DataPublicacaoDecisao).Index(11).TypeConverterOption.Format("dd/MM/yyyy");
                Map(m => m.NumeroDIOEDecisao).Index(12);
                Map(m => m.Decisao).Index(13);
                Map(m => m.InfoAdd).Index(14);
            }
        }





    }
}
