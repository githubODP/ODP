using Domain.GovernoFederal.Entidades;
using Domain.GovernoFederal.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.GovernoFederal.Repositories
{
    public class BeneficioContinuoRepositoryRead : RepositoryRead<BeneficioContinuo>, IBeneficioContinuoRepositoryRead
    {
        public BeneficioContinuoRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<BeneficioContinuo>> BuscarCPF(string cpf)
        {

            return await _context.Set<BeneficioContinuo>()
                .AsNoTracking()
                .Where(c => c.CPF == cpf)
                .Select ( c => new BeneficioContinuo
                {
                    NomeBeneficiario = c.NomeBeneficiario,
                    CPF = c.CPF,
                    NomeMunicipio = c.NomeMunicipio,
                    UFMunicipio = c.UFMunicipio,
                    NumeroBeneficio = c.NumeroBeneficio,
                    Ano = c.Ano,    
                    MesCompetencia = c.MesCompetencia,  
                    ValorParcela = c.ValorParcela,
                })
                .ToListAsync();

        }

        public Task<List<BeneficioContinuo>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

    }
}
