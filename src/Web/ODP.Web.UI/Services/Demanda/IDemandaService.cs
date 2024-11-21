
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Demandas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Demanda

{
    public interface IDemandaService
    {

        Task<PagedResult<DemandaViewModel>> Listar(int pageNumber, int pageSize);
        Task<DemandaViewModel> ObterId(Guid id);
        Task<DemandaViewModel> Adicionar(DemandaViewModel demandaViewModel);
        Task<DemandaViewModel> Alterar(DemandaViewModel demandaViewModel, Guid id);
        Task<DemandaViewModel> Deletar(Guid id);

        Task<List<DemandaViewModel>> BuscarCNPJ(string cnpj);
        Task<List<DemandaViewModel>> BuscarCPF(string cpf);
    }
}
