using ODP.Web.UI.Models.Internos;
using System.Threading.Tasks;
using System;
using ODP.Web.UI.Extensions;

namespace ODP.Web.UI.Services.Internos
{
    public interface IDemandaService
    {


        Task<PagedResult<DemandasViewModel>> Listar(int pageNumber, int pageSize);
        Task<DemandasViewModel> Adicionar(DemandasViewModel demandas);
        Task<DemandasViewModel> Alterar(DemandasViewModel demandas);
        Task<DemandasViewModel> Deletar(DemandasViewModel demandas);
        Task<DemandasViewModel> ObterId(Guid Id);
        Task<DemandasViewModel> BuscarCPF(string cpf);
        Task<DemandasViewModel> BuscarCNPJ(string cnpj);
    }
}
