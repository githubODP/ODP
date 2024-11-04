
using Domain.Library.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Infra.Library.Mapping
{
    public class DocumentoInternoMapping : IEntityTypeConfiguration<DocumentoInterno>
    {
        public void Configure(EntityTypeBuilder<DocumentoInterno> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(builder => builder.DataDocumento).
            HasColumnType("Datetime");
            builder.Property(builder => builder.DadoBusca).
                HasColumnType("nvarchar(max)");

            builder.Property(builder => builder.Motivo).HasColumnType("nvarchar(max)");

            builder.Property(builder => builder.Tipo).HasColumnType("varchar(50)");

            builder.Property(builder => builder.DocumentoAnexo).HasColumnType("nvarchar(max)");

            builder.Property(builder => builder.Solicitacao).HasColumnType("nvarchar(max)");

            builder.ToTable("DocumentosInternos");



        }
    }
}
