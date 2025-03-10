using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Internos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ODP.Web.UI.Services.Internos

{
    public interface IDemandaService
    {

        Task<PagedResult<DemandasViewModel>> Listar(int pageNumber = 1, int pageSize = 5, string termo = null);
        Task<DemandasViewModel> ObterId(Guid id);
        Task<DemandasViewModel> Adicionar(DemandasViewModel demandaViewModel);
        Task<DemandasViewModel> Alterar(Guid id,DemandasViewModel demandaViewModel);
        Task<bool> Deletar(Guid id);

        Task<List<DemandasViewModel>> BuscarCNPJ(string cnpj);
        Task<List<DemandasViewModel>> BuscarCPF(string cpf);

    }
}
