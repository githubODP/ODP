
using CsvHelper;
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

            try
            {
                // Salva o arquivo CSV em um local temporário
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Lê o CSV e converte para a lista de Instauracao
                var instauracoes = await LerCsvParaInstauracoesAsync(filePath);

                // Insere os registros no banco de dados
                foreach (var instauracao in instauracoes)
                {
                    await _instauracaoRepository.Adicionar(instauracao);
                }

                // Retorna sucesso com o número de registros processados
                return Ok(new
                {
                    Message = "Arquivo processado com sucesso.",
                    TotalRegistros = instauracoes.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
            }
        }

        private async Task<List<Instauracao>> LerCsvParaInstauracoesAsync(string filePath)
        {
            var instauracoes = new List<Instauracao>();

            using (var reader = new StreamReader(filePath))
            {
                // Lê a primeira linha para obter os cabeçalhos
                var header = await reader.ReadLineAsync();
                if (header == null)
                {
                    throw new Exception("O arquivo CSV está vazio ou corrompido.");
                }

                // Lê as linhas seguintes e converte para Instauracao
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var values = line.Split(';');
                        var instauracao = new Instauracao
                        {
                            Ano = int.Parse(values[0]),
                            CNPJCPF = values[1],
                            RG = values[2],
                            Orgao = Enum.TryParse<ETipoOrgao>(values[3], out var orgao) ? orgao : throw new Exception($"Valor inválido para Orgao: {values[3]}"),
                            Procedimento = Enum.TryParse<ETipoProcedimento>(values[4], out var procedimento) ? procedimento : throw new Exception($"Valor inválido para Procedimento: {values[4]}"),
                            Protocolo = values[5],
                            Objeto = values[6],
                            AtoNormativo = values[7],
                            DataPublicacao = DateTime.TryParse(values[8], out var dataPub) ? dataPub : (DateTime?)null,
                            NumeroDIOE = int.TryParse(values[9], out var numeroDIOE) ? numeroDIOE : (int?)null,
                            AtoNormativoDecisao = values[10],
                            NumeroDIOEDecisao = int.TryParse(values[11], out var numeroDIOEDecisao) ? numeroDIOEDecisao : (int?)null,
                            DataPublicacaoDecisao = DateTime.TryParse(values[12], out var dataPubDec) ? dataPubDec : (DateTime?)null,
                            Decisao = Enum.TryParse<ETipoDecisao>(values[13], out var decisao) ? decisao : throw new Exception($"Valor inválido para Decisao: {values[13]}"),
                            InfoAdd = values[14]
                        };
                        instauracoes.Add(instauracao);
                    }
                }
            }

            return instauracoes;
        }









    }
}
