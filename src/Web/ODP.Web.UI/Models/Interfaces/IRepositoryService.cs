using ODP.Web.UI.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Web.UI.Models.Interfaces
{
    public interface IRepositoryService<T> where T : class
    {
        Task<PagedResult<T>> Listar(int pageNumber, int pageSize);
        Task<T> ObterId(Guid id);

    }

    public interface IBuscarDados<T> where T : class
    {
        Task<List<T>> BuscarCNPJ(string cnpj);
        Task<List<T>> BuscarCPF(string cpf);
    }




}
