using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Internos.Repositories
{
    public class TermoCooperacaoRepository : Repository<TermoCooperacao>, ITermoCooperacaoRepository
    {
        public TermoCooperacaoRepository(ObservatorioContext context) : base(context) { }
    }
}
