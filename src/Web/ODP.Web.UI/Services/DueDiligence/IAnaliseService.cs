using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.DueDiligence;
using System.Threading.Tasks;
using System;

namespace ODP.Web.UI.Services.DueDiligence
{
    public interface IAnaliseService
    {
        //Task<PagedResult<AnaliseCadastroViewModel>> Listar(int pageNumber = 1, int pageSize = 5);
        Task<PagedResult<AnaliseViewModel>> ListarDadosAdicionais(int pageNumber = 1, int pageSize = 10);

        Task<AnaliseCadastroViewModel> ObterId(Guid id);
        Task<AnaliseCadastroViewModel> Adicionar(AnaliseCadastroViewModel analisecadastroViewModel);
        Task<AnaliseCadastroViewModel> Alterar(AnaliseCadastroViewModel analisecadastroViewModel, Guid id);
        Task<AnaliseCadastroViewModel> Deletar(Guid id);
        Task<AnaliseViewModel> PesquisarComissionado(string nroProtocolo); 



    }
}
