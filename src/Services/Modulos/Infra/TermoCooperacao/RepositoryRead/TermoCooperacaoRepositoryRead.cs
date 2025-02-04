using CGEODP.Core.DomainObjects;
using Domain.TermoCooperacao.Entidade;
using Domain.TermoCooperacao.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.TermoCooperacao.RepositoryRead
{
    public class TermoCooperacaoRepositoryRead : RepositoryRead<Domain.TermoCooperacao.Entidade.TermoCooperacao>, ITermoCooperacaoRepositoryRead, IAggregateRoot 
    {
        public TermoCooperacaoRepositoryRead(ObservatorioContext context):base(context) { }
    }
}
