using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.GovernoFederal.Repositories
{
    public class BolsaFamiliaRepositoryRead : RepositoryRead<BolsaFamilia>, IBolsaFamiliaRepositoryRead
    {
        public BolsaFamiliaRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<BolsaFamilia>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BolsaFamilia>> BuscarCPF(string cpf)
        {
            return await _context.Set<BolsaFamilia>()
               .AsNoTracking()
               .Where(c => c.CPF == cpf)
               .Select(c => new BolsaFamilia
               {
                   Nome = c.Nome,
                   CPF = c.CPF,
                   NomeMunicipio = c.NomeMunicipio,
                   UF = c.UF,
                   AnoReferencia = c.AnoReferencia,
                   MesReferencia = c.MesReferencia,
                   Valor = c.Valor,

               })
               .ToListAsync();
        }
    }
}
