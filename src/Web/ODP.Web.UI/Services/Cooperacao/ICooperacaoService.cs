﻿using ODP.Web.UI.Extensions;
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
        Task<TermoCooperacaoViewModel> Alterar(TermoCooperacaoViewModel termo);
        Task<TermoCooperacaoViewModel> Deletar(TermoCooperacaoViewModel termo);
        Task<TermoCooperacaoViewModel> ObterProtocolo(string protocolo);
        Task<TermoCooperacaoViewModel> ObterId(Guid Id);

        Task<List<TermoCooperacaoViewModel>> VerificarAlertasFimVigencia();

    }
}
