using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Domain.Corregedoria.Entidade;
using Domain.Corregedoria.Enum;
using Domain.Corregedoria.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/corregedoria")]
    [Authorize]
    public class CorregedoriaController : ControllerBase
    {
        private readonly IInstauracaoRepository _instauracaoRepository;
        private readonly IInstauracaoRepositoryRead _instauracaoRepositoryRead;
        private readonly ILogger<CorregedoriaController> _logger;


        public CorregedoriaController(IInstauracaoRepository instauracaoRepository,
                                      IInstauracaoRepositoryRead instauracaoRepositoryRead,
                                      ILogger<CorregedoriaController> logger)
        {
            _instauracaoRepository = instauracaoRepository;
            _instauracaoRepositoryRead = instauracaoRepositoryRead;
            _logger = logger;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(
            int pageNumber = 1,
            int pageSize = 8,
            int? ano = null,
            string orgao = null,
            string procedimento = null,
            string decisao = null,
            string protocolo = null)
        {
            _logger.LogInformation("Entrou no método Index da API com parâmetros: pageNumber={PageNumber}, pageSize={PageSize}, ano={Ano}, orgao={Orgao}, procedimento={Procedimento}, decisao={Decisao}, protocolo={Protocolo}", pageNumber, pageSize, ano, orgao, procedimento, decisao, protocolo);

            try
            {
                var pagedResult = await _instauracaoRepositoryRead.ListarComFiltrosAsync(
                    pageNumber,
                    pageSize,
                    ano,
                    orgao,
                    procedimento,
                    decisao,
                    protocolo);

                _logger.LogInformation("Retornou {Count} resultados do repositório.", pagedResult.Results.Count);

                return Ok(pagedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no método Index da API.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }




        // Buscar por ID
        [HttpGet("buscaId/{id}")]
        public async Task<IActionResult> BuscaId(Guid id)
        {
            var instauracao = await _instauracaoRepositoryRead.ObterId(id);
            if (instauracao == null)
                return NotFound("Registro não encontrado.");

            return Ok(instauracao);
        }

        // Buscar por CNPJ
        [HttpGet("consultacnpj/{cnpj}")]
        public async Task<IActionResult> SearchCNPJ(string cnpj)
        {
            var instauracao = await _instauracaoRepositoryRead.BuscarPorCNPJ(cnpj);
            if (instauracao == null)
                return NotFound("Nenhum registro encontrado para o CNPJ informado.");

            return Ok(instauracao);
        }

        // Buscar por CPF
        [HttpGet("consultacpf/{cpf}")]
        public async Task<IActionResult> SearchCPF(string cpf)
        {
            var instauracao = await _instauracaoRepositoryRead.BuscarPorCPF(cpf);
            if (instauracao == null)
                return NotFound("Nenhum registro encontrado para o CPF informado.");

            return Ok(instauracao);
        }

        // Adicionar nova instauração
        [HttpPost("adicionar")]
        public async Task<IActionResult> Add([FromBody] Instauracao instauracao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica se o Procedimento é válido para TAC
            var procedimentosValidos = new[] { "TAC_CELEBRADO", "TAC_CONCLUIDO", "TAC_DESCUMPRIDO", "TAC_INVIAVEL" };
            if (!procedimentosValidos.Contains(instauracao.Decisao.ToString()))
            {
                // Zera os campos relacionados ao TAC, caso Decisao não seja válida
                instauracao.DataInicioTac = null;
                instauracao.DataFimTac = null;
                instauracao.PrazoEncerra = null;
                instauracao.PGE = null;
                instauracao.Cumpriu = null;
                instauracao.ObservacaoAjusteTAC = null;
                instauracao.Obrigacao = null;
            }
            else
            {
                // Calcula o PrazoEncerra automaticamente, se DataInicioTac e DataFimTac forem fornecidos
                if (instauracao.DataInicioTac.HasValue && instauracao.DataFimTac.HasValue)
                {
                    instauracao.PrazoEncerra = (instauracao.DataFimTac.Value - instauracao.DataInicioTac.Value).Days;
                }
            }

            try
            {
                await _instauracaoRepository.Adicionar(instauracao);
                return CreatedAtAction(nameof(BuscaId), new { id = instauracao.Id }, instauracao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar registro: {ex.Message}");
            }
        }





        [HttpPost("alterar/{id}")]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] Instauracao instauracao)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != instauracao.Id)
                return BadRequest("O ID fornecido não corresponde ao registro.");

            // Verifica se a entidade existe no banco
            var existente = await _instauracaoRepositoryRead.ObterId(id);
            if (existente == null)
                return NotFound("Registro não encontrado.");

            try
            {
                // Atualiza TODOS os campos comuns da entidade existente
                existente.Ano = instauracao.Ano;
                existente.CNPJCPF = instauracao.CNPJCPF;
                existente.RG = instauracao.RG;
                existente.Orgao = instauracao.Orgao;
                existente.Procedimento = instauracao.Procedimento;
                existente.Protocolo = instauracao.Protocolo;
                existente.Objeto = instauracao.Objeto;
                existente.AtoNormativo = instauracao.AtoNormativo;
                existente.DataPublicacao = instauracao.DataPublicacao;
                existente.NumeroDIOE = instauracao.NumeroDIOE;
                existente.AtoNormativoDecisao = instauracao.AtoNormativoDecisao;
                existente.DataPublicacaoDecisao = instauracao.DataPublicacaoDecisao;
                existente.NumeroDIOEDecisao = instauracao.NumeroDIOEDecisao;
                existente.Decisao = instauracao.Decisao;
                existente.InfoAdd = instauracao.InfoAdd;

                // Define quais opções do Enum habilitam os campos TAC
                var decisoesValidas = new[] { "TAC_CELEBRADO", "TAC_CONCLUIDO", "TAC_DESCUMPRIDO", "TAC_INVIAVEL" };

                if (decisoesValidas.Contains(instauracao.Decisao.ToString()))
                {
                    // Preenche os campos TAC se a decisão estiver dentro das válidas
                    existente.DataInicioTac = instauracao.DataInicioTac;
                    existente.DataFimTac = instauracao.DataFimTac;
                    existente.PGE = instauracao.PGE;
                    existente.Cumpriu = instauracao.Cumpriu;
                    existente.Obrigacao = instauracao.Obrigacao;
                    existente.ObservacaoAjusteTAC = instauracao.ObservacaoAjusteTAC;

                    // Calcula o PrazoEncerra somente se houver DataInicioTac e DataFimTac
                    if (instauracao.DataInicioTac.HasValue && instauracao.DataFimTac.HasValue)
                    {
                        existente.PrazoEncerra = (int)(instauracao.DataFimTac.Value - instauracao.DataInicioTac.Value).TotalDays;
                    }
                    else
                    {
                        existente.PrazoEncerra = null;
                    }
                }
                else
                {
                    // Zera os campos TAC se a decisão não estiver dentro das válidas
                    existente.DataInicioTac = null;
                    existente.DataFimTac = null;
                    existente.PGE = null;
                    existente.Cumpriu = null;
                    existente.PrazoEncerra = null;
                    existente.Obrigacao = null;
                    existente.ObservacaoAjusteTAC = null;
                }

                // Atualiza no banco de dados
                await _instauracaoRepository.Atualizar(existente);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o registro: {ex.Message}");
            }
        }



        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var termoExistente = await _instauracaoRepositoryRead.ObterId(id);
            if (termoExistente == null)
            {
                return NotFound("Termo de cooperação não encontrado.");
            }

            await _instauracaoRepository.Deletar(termoExistente);
            return NoContent(); // Retorna 204 (No Content) para indicar sucesso
        }



        [HttpPost("uploadcsv")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Por favor, selecione um arquivo CSV válido.");

            var instauracoes = new List<Instauracao>();
            var linhasComErro = new List<string>();

            try
            {
                // 1. Salva o arquivo temporariamente
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // 2. Configuração do CsvHelper
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ";",
                    TrimOptions = TrimOptions.Trim,
                    BadDataFound = args =>
                    {
                        linhasComErro.Add($"Linha {args.Context.Parser.RawRow}: Dado ruim encontrado: {args.Field}");
                    },
                    MissingFieldFound = null,
                    Encoding = Encoding.UTF8
                };

                // 3. Processa o arquivo CSV
                using (var reader = new StreamReader(filePath, Encoding.UTF8))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<InstauracaoMap>();

                    while (await csv.ReadAsync())
                    {
                        try
                        {
                            var instauracao = csv.GetRecord<Instauracao>();

                            // Validação dos campos TAC
                            if (instauracao.Decisao == ETipoDecisao.TAC_CELEBRADO ||
                                instauracao.Decisao == ETipoDecisao.TAC_CONCLUIDO ||
                                instauracao.Decisao == ETipoDecisao.TAC_DESCUMPRIDO ||
                                instauracao.Decisao == ETipoDecisao.TAC_INVIAVEL)
                            {
                                if (instauracao.DataInicioTac == null || instauracao.DataFimTac == null)
                                {
                                    linhasComErro.Add($"Erro na linha {csv.Context.Parser.RawRow}: Campos TAC inválidos.");
                                    continue;
                                }
                            }

                            instauracoes.Add(instauracao);
                        }
                        catch (Exception ex)
                        {
                            linhasComErro.Add($"Erro na linha {csv.Context.Parser.RawRow}: {ex.Message}");
                        }
                    }
                }

                // 4. Adiciona ao banco em lote
                if (instauracoes.Any())
                {
                    await _instauracaoRepository.AdicionarEmLoteAsync(instauracoes);
                }

                // 5. Retorna a resposta com informações de sucesso e erro
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




        public sealed class InstauracaoMap : ClassMap<Instauracao>
        {
            public InstauracaoMap()
            {
                Map(m => m.Ano).Index(0).TypeConverterOption.NullValues("0");
                Map(m => m.CNPJCPF).Index(1);
                Map(m => m.RG).Index(2);

                // Usando o conversor genérico para enums
                Map(m => m.Orgao).Index(3).TypeConverter<GenericEnumConverter<ETipoOrgao>>();
                Map(m => m.Procedimento).Index(4).TypeConverter<GenericEnumConverter<ETipoProcedimento>>();
                Map(m => m.Decisao).Index(13).TypeConverter<GenericEnumConverter<ETipoDecisao>>();

                Map(m => m.Protocolo).Index(5);
                Map(m => m.Objeto).Index(6);
                Map(m => m.AtoNormativo).Index(7);
                Map(m => m.DataPublicacao).Index(8).TypeConverterOption.Format("dd/MM/yyyy");
                Map(m => m.NumeroDIOE).Index(9).TypeConverterOption.NullValues("0");
                Map(m => m.AtoNormativoDecisao).Index(10);
                Map(m => m.DataPublicacaoDecisao).Index(11).TypeConverterOption.Format("dd/MM/yyyy");
                Map(m => m.NumeroDIOEDecisao).Index(12).TypeConverterOption.NullValues("0");

                // Campos TAC
                Map(m => m.Obrigacao).Index(15);
                Map(m => m.DataInicioTac).Index(16).TypeConverterOption.Format("dd/MM/yyyy");
                Map(m => m.DataFimTac).Index(17).TypeConverterOption.Format("dd/MM/yyyy");
                Map(m => m.PrazoEncerra)
                  .Index(18)
                  .TypeConverterOption.NullValues(string.Empty) // Trata strings vazias como nulos
                  .TypeConverterOption.NullValues("0");         // Ou strings com "0"

                // Booleans usando conversor personalizado
                Map(m => m.PGE).Index(19).TypeConverter<BooleanConverter>();
                Map(m => m.Cumpriu).Index(20).TypeConverter<BooleanConverter>();
                Map(m => m.ObservacaoAjusteTAC).Index(21);
            }
        }




        public class BooleanConverter : DefaultTypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (string.IsNullOrWhiteSpace(text))
                    return false; // Retorna false para valores vazios ou nulos.

                text = text.Trim().ToLower();
                return text switch
                {
                    "1" or "true" or "sim" => true,
                    "0" or "false" or "nao" or "não" => false,
                    _ => throw new FormatException($"Valor inválido para booleano: {text}")
                };
            }
        }




        public class GenericEnumConverter<T> : DefaultTypeConverter where T : struct, Enum
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (string.IsNullOrWhiteSpace(text))
                    throw new FormatException($"O valor para o enum '{typeof(T).Name}' está vazio ou nulo.");

                if (Enum.TryParse(typeof(T), text, true, out var result))
                    return result;

                throw new FormatException($"Valor inválido '{text}' para o enum '{typeof(T).Name}'.");
            }
        }






    }
}

