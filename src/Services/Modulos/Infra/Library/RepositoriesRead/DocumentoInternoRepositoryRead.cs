using CGEODP.Core.DomainObjects;
using Domain.Library.Entidades;
using Domain.Library.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;


namespace Library.Infra.RepositoriesRead
{
    public class DocumentoInternoRepositoryRead : RepositoryRead<DocumentoInterno>, IDocumentoInternoRepositoryRead, IAggregateRoot
    {
        public DocumentoInternoRepositoryRead(ObservatorioContext context) : base(context) { }
    }
}
