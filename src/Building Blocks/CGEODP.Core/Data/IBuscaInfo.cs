using CGEODP.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGEODP.Core.Data
{
    public  interface IBuscaInfo<T> : IDisposable where T : class, IAggregateRoot
    {
        Task<List<T>> BuscarCNPJ(string cnpj);
        Task<List<T>> BuscarCPF(string cpf);
    }
}
