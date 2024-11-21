using CGEODP.Core.DomainObjects;
using Domain.Compras.Entidades;
using Domain.Compras.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Compras.RepositoriesRead
{
    public class DispensaRepositoryRead : RepositoryRead<Dispensa>, IDispensaRepositoryRead, IAggregateRoot
    {
        public DispensaRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<List<Dispensa>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<Dispensa>()
            .AsNoTracking()
            .Where(c => c.CNPJ == cnpj)
            .Select(c => new Dispensa
            {
                Orgao = c.Orgao,
                Fornecedor = c.Fornecedor,
                CNPJ = c.CNPJ,
                Protocolo = c.Protocolo,
                Modalidade = c.Modalidade,
                ValorItem = c.ValorItem,
                ValorDispensa = c.ValorDispensa,
                DataDispensa = c.DataDispensa,

            })

            .ToListAsync();
        }

        public Task<List<Dispensa>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
