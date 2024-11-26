
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Corregedoria;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ODP.Web.UI.Services.Corregedoria
{
    public interface IInstauracaoService
    {
        Task<PagedResult<InstauracaoViewModel>> Listar(int pageNumber, int pageSize);
        Task<InstauracaoViewModel> ObterId(Guid id);
        Task<InstauracaoViewModel> Adicionar(InstauracaoViewModel instauracaoViewModel);
        Task<InstauracaoViewModel> Alterar(InstauracaoViewModel instauracaoViewModel, Guid id);
        Task<InstauracaoViewModel> Deletar(Guid id);
        Task<bool> UploadCsv(IFormFile file);






        /// //Gerador de Arquivo PDF ///

        Task<FileStreamResult> GerarPdf(Guid id);

    }


}


