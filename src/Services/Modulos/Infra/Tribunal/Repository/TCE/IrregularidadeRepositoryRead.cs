using Domain.Tribunal.Entidades.TCE;
using Domain.Tribunal.Interfaces.TCE;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;

namespace Infra.Tribunal.Repository.TCE
{
    public class IrregularidadeRepositoryRead : RepositoryRead<Irregularidade>, IIrregularidadeRepositoryRead
    {
        public IrregularidadeRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public Task<List<Irregularidade>> BuscarCNPJ(string cnpj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Irregularidade>> BuscarCPF(string cpf)
        {
            return await _context.Set<Irregularidade>()
                                 .AsNoTracking()
                                 .Where(c => c.CPF == cpf)
            .Select(c => new Irregularidade
            {
                Nome = c.Nome,
                CPF = c.CPF,
                Cargo = c.Cargo,
                InicioVigencia = c.InicioVigencia,
                TerminoVigencia = c.TerminoVigencia,
                DataPublicacao = c.DataPublicacao,
                Descricao = c.Descricao,
                CNPJ = c.CNPJ,
                Entidade = c.Entidade,
                Processo = c.Processo,
                Decisao = c.Decisao,
                Tipo = c.Tipo,

            })
            .ToListAsync();
        }


    }
}
