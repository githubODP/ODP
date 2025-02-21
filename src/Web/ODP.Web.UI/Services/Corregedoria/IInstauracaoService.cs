
using Microsoft.AspNetCore.Http;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Corregedoria;
using System;
using System.Threading.Tasks;


namespace ODP.Web.UI.Services.Corregedoria
{
    public interface IInstauracaoService
    {
        Task<PagedResult<InstauracaoViewModel>> ListarComFiltros(
        int pageNumber,
        int pageSize,
        int? ano = null,
        string orgao = null,
        string procedimento = null,
        string decisao = null,
        string protocolo = null);
        Task<InstauracaoViewModel> ObterId(Guid id);
        Task<InstauracaoViewModel> Adicionar(InstauracaoViewModel instauracaoViewModel);
        Task<InstauracaoViewModel> Alterar(InstauracaoViewModel instauracaoViewModel, Guid id);
        Task<InstauracaoViewModel> Deletar(Guid id);


        Task<bool> UploadCsv(IFormFile file);







    }


}


