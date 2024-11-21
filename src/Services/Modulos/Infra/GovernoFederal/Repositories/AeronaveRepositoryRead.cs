using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Infra.GovernoFederal.Repositories
{
    public class AeronaveRepositoryRead : RepositoryRead<Aeronave>, IAeronaveRepositoryRead
    {
        public AeronaveRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<Aeronave>> BuscarCNPJ(string cnpj)
        {

            // Decodifica o CNPJ, caso esteja URL-encoded
            cnpj = HttpUtility.UrlDecode(cnpj);

            return await _context.Set<Aeronave>()
                .AsNoTracking()
                .Where(c => c.CNPJCPF == cnpj)
                .Select(c => new Aeronave
                {
                    Proprietario = c.Proprietario,
                    CNPJCPF = c.CNPJCPF,
                    Marca = c.Marca,
                    Operador = c.Operador,
                    CPFCGC = c.CPFCGC,
                })
                .ToListAsync();

        }

        public async Task<List<Aeronave>> BuscarCPF(string cpf)
        {

            return await _context.Set<Aeronave>()
                .AsNoTracking()
                .Where(c => c.CNPJCPF == cpf)
                 .Select(c => new Aeronave
                 {
                     Proprietario = c.Proprietario,
                     CNPJCPF = c.CNPJCPF,
                     Marca = c.Marca,
                     Operador = c.Operador,
                     CPFCGC = c.CPFCGC,
                 })
                .ToListAsync();

        }


    }
}
