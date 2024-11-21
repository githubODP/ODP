using Domain.Tribunal.Entidades.TCE;
using Domain.Tribunal.Interfaces.TCE;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.Tribunal.Repository.TCE
{
    public class InadimplenteRepositoryRead : RepositoryRead<Inadimplente>, IInadimplenteRepositoryRead
    {
        public InadimplenteRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<Inadimplente>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlDecode(cnpj);
            return await _context.Set<Inadimplente>()
                                 .AsNoTracking()
                                 .Where(c => c.CNPJCPF == cnpj)
            .Select(c => new Inadimplente
            {
                Devedor = c.Devedor,
                CNPJCPF = c.CNPJCPF,
                Credor = c.Credor,
                Processo = c.Processo,
                TipoSancao = c.TipoSancao,
                Referencia = c.Referencia,
                Valor = c.Valor,
                ValorRecolhido = c.ValorRecolhido,
                SaldoDevedor = c.SaldoDevedor,

            })
            .ToListAsync();
        }

        public async Task<List<Inadimplente>> BuscarCPF(string cpf)
        {
            return await _context.Set<Inadimplente>()
                                .AsNoTracking()
                                .Where(c => c.CNPJCPF == cpf)
           .Select(c => new Inadimplente
           {
               Devedor = c.Devedor,
               CNPJCPF = c.CNPJCPF,
               Credor = c.Credor,
               Processo = c.Processo,
               TipoSancao = c.TipoSancao,
               Referencia = c.Referencia,
               Valor = c.Valor,
               ValorRecolhido = c.ValorRecolhido,
               SaldoDevedor = c.SaldoDevedor,

           })
            .ToListAsync();
        }


    }
}
