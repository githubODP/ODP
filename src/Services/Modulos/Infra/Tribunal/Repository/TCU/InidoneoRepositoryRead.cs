using Domain.Tribunal.Entidades.TCU;
using Domain.Tribunal.Interfaces.TCU;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System.Web;


namespace Infra.Tribunal.Repository.TCU
{
    public class InidoneoRepositoryRead : RepositoryRead<Inidoneo>, IInidoneoRepositoryRead
    {
        public InidoneoRepositoryRead(ObservatorioContext context) : base(context)
        {
        }

        public async Task<List<Inidoneo>> BuscarCNPJ(string cnpj)
        {
            cnpj = HttpUtility.UrlDecode(cnpj);
            return await _context.Set<Inidoneo>()
                                 .AsNoTracking()
                                 .Where(c => c.CNPJ == cnpj)
            .Select(c => new Inidoneo
             {
                 Nome = c.Nome,
                 CNPJ = c.CNPJ,
                 Processo = c.Processo,
                 Deliberacao = c.Deliberacao,
                 DataFinal = c.DataFinal,   
             })
            .ToListAsync();
        }

        public Task<List<Inidoneo>> BuscarCPF(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
