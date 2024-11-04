using Domain.Tribunal.Entidades.TCU;
using Domain.Tribunal.Interfaces.TCU;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Tribunal.Repository.TCU
{
    public class ContaIrregularRepositoryRead : RepositoryRead<ContaIrregular>, IContaIrregularRepositoryRead
    {
        public ContaIrregularRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<ContaIrregular>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlDecode(cnpj);
            return await _context.Set<ContaIrregular>()
                                 .AsNoTracking()
                                 .Where(c => c.CNPJCPF == cnpj)
            .Select(c => new ContaIrregular
            {

                Nome = c.Nome,
                CNPJCPF = c.CNPJCPF,
                Municipio = c.Municipio,
                UF = c.UF,
                Deliberacao = c.Deliberacao,
                DataJulgado = c.DataJulgado,
            })
            .ToListAsync();
        }

        public async Task<List<ContaIrregular>> BuscarCPF(string cpf)
        {
            return await _context.Set<ContaIrregular>()
                                .AsNoTracking()
                                .Where(c => c.CNPJCPF == cpf)
            .Select(c => new ContaIrregular
            {

                Nome = c.Nome,
                CNPJCPF = c.CNPJCPF,
                Municipio = c.Municipio,
                UF = c.UF,
                Deliberacao = c.Deliberacao,
                DataJulgado = c.DataJulgado,
            })
            .ToListAsync();
        }

    }
}