using CGEODP.Core.DomainObjects;
using Domain.GovernoEstadual.Entidades;
using Domain.GovernoEstadual.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.GovernoEstadual.Repositories
{
    public class AmbientalRepositoryRead : RepositoryRead<Ambiental>, IAmbientalRepositoryRead, IAggregateRoot
    {
        public AmbientalRepositoryRead(ObservatorioContext context) : base(context) { }

        public async Task<List<Ambiental>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<Ambiental>()
                .AsNoTracking()
                .Where(c => c.CNPJCPF == cnpj)
                .Select (c => new Ambiental
                {
                    Infrator = c.Infrator,
                    CNPJCPF = c.CNPJCPF,
                    Situacao = c.Situacao,
                    AnoInfracao = c.AnoInfracao,    
                })
                .ToListAsync();

        }

        public async Task<List<Ambiental>> BuscarCPF(string cpf)
        {

            return await _context.Set<Ambiental>()
                .AsNoTracking()
                .Where(c => c.CNPJCPF == cpf)
                .Select(c => new Ambiental
                {
                    Infrator = c.Infrator,
                    CNPJCPF = c.CNPJCPF,
                    Situacao = c.Situacao,
                    AnoInfracao = c.AnoInfracao,
                })
                .ToListAsync(); 

        }

    }
}
