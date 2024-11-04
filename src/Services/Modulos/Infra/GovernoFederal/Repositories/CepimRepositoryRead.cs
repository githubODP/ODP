using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infra.GovernoFederal.Repositories
{
    public class CepimRepositoryRead : RepositoryRead<Cepim>, ICepimRepositoryRead
    {
        public CepimRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<Cepim>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<Cepim>()
                .AsNoTracking()
                .Where(c => c.CNPJ == cnpj)
                .Select(c => new Cepim
                {
                    Nome = c.Nome,
                    CNPJ = c.CNPJ,
                    NroConvenio = c.NroConvenio,
                    Orgao = c.Orgao,
                    Impedimento = c.Impedimento,
                })
                .ToListAsync();

        }

        public Task<List<Cepim>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
