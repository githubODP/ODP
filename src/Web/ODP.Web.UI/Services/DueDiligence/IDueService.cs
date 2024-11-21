

using Microsoft.AspNetCore.Mvc;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.DueDiligence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.DueDiligence
{
    public interface IDueService
    {
        Task<PagedResult<DueDiligenceViewModel>> Listar(int pageNumber, int pageSize);
        Task<DueDiligenceViewModel> ObterId(Guid id);
        Task<DueDiligenceViewModel> Adicionar(DueDiligenceViewModel dueViewModel);
        Task<DueDiligenceViewModel> Alterar(DueDiligenceViewModel dueViewModel, Guid id);
        Task<DueDiligenceViewModel> Deletar(Guid id);

        Task<List<DueDiligenceViewModel>> BuscarPorCPF(string cpf);

        Task<FileStreamResult> GerarPdf(Guid id);
    }
}
