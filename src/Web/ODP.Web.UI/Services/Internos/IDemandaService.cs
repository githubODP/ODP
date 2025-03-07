using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Internos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Internos

{
    public interface IDemandaService
    {

        Task<PagedResult<DemandaViewModel>> Listar(int pageNumber = 1, int pageSize = 5, string termo = null);
        Task<DemandaViewModel> ObterId(Guid id);
        Task<DemandaViewModel> Adicionar(DemandaViewModel demandaViewModel);
        Task<DemandaViewModel> Alterar(Guid id,DemandaViewModel demandaViewModel);
        Task<bool> Deletar(Guid id);

        Task<List<DemandaViewModel>> BuscarCNPJ(string cnpj);
        Task<List<DemandaViewModel>> BuscarCPF(string cpf);
    }
}
