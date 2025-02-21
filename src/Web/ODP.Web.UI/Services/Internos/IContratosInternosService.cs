using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Cooperacao;
using ODP.Web.UI.Models.Internos;
using System;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Internos
{
    public interface IContratosInternosService
    {

        Task<PagedResult<ContratosInternosViewModel>> Listar(int pageNumber, int pageSize);
        Task<ContratosInternosViewModel> Adicionar(ContratosInternosViewModel contratos);
        Task<ContratosInternosViewModel> Alterar(ContratosInternosViewModel contratos);
        Task<ContratosInternosViewModel> Deletar(ContratosInternosViewModel contratos);
        Task<ContratosInternosViewModel> ObterId(Guid Id);
    }
}
