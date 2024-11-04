

using Domain.Library.Entidades;
using Domain.Library.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;

namespace Library.Infra.Repositories
{
    public class DocumentoInternoRepository : Repository<DocumentoInterno>, IDocumentoInternoRepository
    {
        public DocumentoInternoRepository(ObservatorioContext context) : base(context) { }
    }

}
