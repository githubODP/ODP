using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Cooperacao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Cooperacao
{
    public interface ICooperacaoService
    {
        Task<PagedResult<TermoCooperacaoViewModel>> Listar(int pageNumber =1, int pageSize = 5, string termo = null);       
        Task<TermoCooperacaoViewModel> Adicionar(TermoCooperacaoViewModel termo);
        Task<TermoCooperacaoViewModel> Alterar(Guid id, TermoCooperacaoViewModel termo);
        Task<bool> Deletar(Guid id);       
        Task<TermoCooperacaoViewModel> ObterId(Guid id);
        Task<List<TermoCooperacaoViewModel>> VerificarAlertasFimVigencia();

    }
}
