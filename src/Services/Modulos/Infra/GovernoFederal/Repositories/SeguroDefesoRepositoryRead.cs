using CGEODP.Core.Data;
using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.GovernoFederal.Repositories
{
    public class SeguroDefesoRepositoryRead : RepositoryRead<SeguroDefeso>, ISeguroDefesoRepositoryRead
    {
        public SeguroDefesoRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<SeguroDefeso>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SeguroDefeso>> BuscarCPF(string cpf)
        {

            return await _context.Set<SeguroDefeso>()
                .AsNoTracking()
                .Where(c => c.CPF == cpf)
                .Select (c => new SeguroDefeso
                {
                    NomeFavorecido = c.NomeFavorecido,
                    CPF = c.CPF,
                    NomeMunicipio = c.NomeMunicipio,    
                    UF = c.UF,
                    Ano = c.Ano,
                    Mes = c.Mes,
                    ValorParcela = c.ValorParcela,
                })
                .ToListAsync();

        }

        
    }
}
