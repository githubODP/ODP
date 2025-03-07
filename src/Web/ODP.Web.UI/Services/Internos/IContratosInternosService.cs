using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Internos;
using System;
using System.Threading.Tasks;


namespace ODP.Web.UI.Services.Internos
{
    public interface IContratosInternosService
    {

        Task<PagedResult<ContratosInternosViewModel>> Listar(int pageNumber = 1, int pageSize = 5, string termo = null);
        Task<ContratosInternosViewModel> Adicionar(ContratosInternosViewModel termo);
        Task<ContratosInternosViewModel> Alterar(Guid id, ContratosInternosViewModel termo);
        Task<bool> Deletar(Guid id);
        Task<ContratosInternosViewModel> ObterId(Guid id);
    }
}
