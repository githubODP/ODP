using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.DueDiligence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.DueDiligence
{
    public interface IAnaliseService
    {

        Task<PagedResult<AnaliseViewModel>> ListarDadosAdicionais(int pageNumber = 1, int pageSize = 10);

        Task<AnaliseCadastroViewModel> ObterId(Guid id);
        Task<AnaliseCadastroViewModel> Adicionar(AnaliseCadastroViewModel analiseCadastroViewModel);
        Task<AnaliseCadastroViewModel> Alterar(AnaliseCadastroViewModel analiseCadastroViewModel, Guid id);
        Task<AnaliseCadastroViewModel> Deletar(Guid id);
        Task<List<DueDiligenceViewModel>> PesquisarComissionado(string nroProtocolo);




    }
}
