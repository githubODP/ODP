using Domain.Library.Entidades;
using Domain.Library.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Library.API.Controllers
{

    [ApiController]
    [Route("api/library")]
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly IDocumentoInternoRepository _documentoInternoRepository;
        private readonly IDocumentoInternoRepositoryRead _documentoInternoRepositoryRead;

        public LibraryController(IDocumentoInternoRepository documentoInternoRepository, IDocumentoInternoRepositoryRead documentoInternoRepositoryRead)
        {
            _documentoInternoRepository = documentoInternoRepository;
            _documentoInternoRepositoryRead = documentoInternoRepositoryRead;

        }

        [HttpGet("listar")]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = await _documentoInternoRepositoryRead.Listar(pageNumber, pageSize);
            return Ok(pagedResult);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromForm] DocumentoInternoViewModel documento)
        {


            if (documento.DocumentoAnexo == null || documento.DocumentoAnexo.Length == 0)
            {
                return BadRequest("Nenhum arquivo  anexado.");
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(uploadPath)) { Directory.CreateDirectory(uploadPath); }

            var filePath = Path.Combine(uploadPath, documento.DocumentoAnexo.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await documento.DocumentoAnexo.CopyToAsync(stream);
            }


            var docBd = new DocumentoInterno
            {
                Solicitacao = documento.Solicitacao,
                Tipo = documento.Tipo,
                DataDocumento = documento.DataDocumento,
                Motivo = documento.Motivo,
                DadoBusca = documento.DadoBusca,
                DocumentoAnexo = filePath
            };

            await _documentoInternoRepository.Adicionar(docBd);

            return Ok();

        }


        [HttpGet("download/{documentoId}")]
        public async Task<IActionResult> Download(Guid documentoId)
        {
            // 1. Obter o caminho do arquivo a partir do ID do documento
            var documento = await _documentoInternoRepositoryRead.ObterId(documentoId);

            if (documento == null || string.IsNullOrEmpty(documento.DocumentoAnexo))
            {
                return NotFound("Documento ou arquivo não encontrado.");
            }

            var caminhoCompleto = documento.DocumentoAnexo;

            // 2. Verificar se o arquivo existe
            if (!System.IO.File.Exists(caminhoCompleto))
            {
                return NotFound("Arquivo não encontrado.");
            }

            // 3. Obter o tipo de conteúdo e o nome do arquivo
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(caminhoCompleto, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var nomeArquivo = Path.GetFileName(caminhoCompleto);

            // 4. Ler o arquivo e retornar o resultado
            var bytes = await System.IO.File.ReadAllBytesAsync(caminhoCompleto);
            return File(bytes, contentType, nomeArquivo);
        }


        [HttpGet("buscaId/{id}")]
        public async Task<DocumentoInterno> BuscaId(Guid id)
        {
            return await _documentoInternoRepositoryRead.ObterId(id);
        }




        [HttpPost("alterar")]
        public async Task<IActionResult> Update([FromForm] DocumentoInternoViewModel documento)
        {
            // Verifica se o arquivo anexo não é nulo e tem um tamanho maior que zero
            if (documento.DocumentoAnexo != null && documento.DocumentoAnexo.Length > 0)
            {
                // Busca o documento no banco de dados usando o Id
                var documentosInternos = await BuscaId(documento.Id);

                // Verifica se o arquivo anexo é diferente do atual
                if (documentosInternos != null &&
                    documentosInternos.DocumentoAnexo != null &&
                    Path.GetFileName(documentosInternos.DocumentoAnexo) != documento.DocumentoAnexo.FileName)
                {
                    // Exclui o arquivo antigo
                    System.IO.File.Delete(documentosInternos.DocumentoAnexo);

                    // Define o caminho de upload
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    // Salva o novo arquivo
                    var filePath = Path.Combine(uploadPath, documento.DocumentoAnexo.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await documento.DocumentoAnexo.CopyToAsync(stream);
                    }

                    // Atualiza os dados do documento no banco de dados
                    documentosInternos.Solicitacao = documento.Solicitacao;
                    documentosInternos.Tipo = documento.Tipo;
                    documentosInternos.DataDocumento = documento.DataDocumento;
                    documentosInternos.Motivo = documento.Motivo;
                    documentosInternos.DadoBusca = documento.DadoBusca;
                    documentosInternos.DocumentoAnexo = filePath;

                    await _documentoInternoRepository.Atualizar(documentosInternos);
                    return Ok();
                }
            }

            // Caso o arquivo seja o mesmo ou não tenha sido enviado, apenas atualize os outros campos
            var documentosI = await BuscaId(documento.Id);
            if (documentosI != null)
            {
                documentosI.Solicitacao = documento.Solicitacao;
                documentosI.Tipo = documento.Tipo;
                documentosI.DataDocumento = documento.DataDocumento;
                documentosI.Motivo = documento.Motivo;
                documentosI.DadoBusca = documento.DadoBusca;
                documentosI.DocumentoAnexo = documentosI.DocumentoAnexo; // mantém o mesmo arquivo

                await _documentoInternoRepository.Atualizar(documentosI);
            }

            return Ok();
        }

    }
}
