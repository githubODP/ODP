using Domain.Tribunal.Entidades.TCE;
using Domain.Tribunal.Interfaces.TCE;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.Tribunal.Repository.TCE
{
    public class CPFRestritoRepositoryRead : RepositoryRead<CPFRestrito>, ICPFRestritoRepositoryRead
    {
        public CPFRestritoRepositoryRead(ObservatorioContext context) : base(context)
        {

        }

        public Task<List<CPFRestrito>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }



        public async Task<List<CPFRestrito>> BuscarCPF(string cpf)
        {
            return await _context.Set<CPFRestrito>()
                                 .AsNoTracking()
                                 .Where(c => c.CPF == cpf)
            .Select(c => new CPFRestrito
            {
                CPF = c.CPF,
                NomeRazaoSocial = c.NomeRazaoSocial,
                TipoSancao = c.TipoSancao,
                DataInicio = c.DataInicio,
                DataFim = c.DataFim,
                Situacao = c.Situacao,
                Municipio = c.Municipio,
            })
            .ToListAsync();
        }


    }
}
