using Domain.Tribunal.Entidades.TCE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TCE
{
    public class IrregularidadeMapping : IEntityTypeConfiguration<Irregularidade>
    {
        public void Configure(EntityTypeBuilder<Irregularidade> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Nome)
              .IsRequired()
              .HasColumnType("nvarchar(500)");

            builder.Property(c => c.CPF)
              .IsRequired()
              .HasColumnType("varchar(14)");

            builder.Property(c => c.Cargo)
              .IsRequired()
              .HasColumnType("varchar(50)");

            builder.Property(c => c.InicioVigencia)
              .IsRequired()
              .HasColumnType("DateTime");

            builder.Property(c => c.TerminoVigencia)
              .IsRequired()
              .HasColumnType("DateTime");

            builder.Property(c => c.Decisao)
              .IsRequired()
              .HasColumnType("nvarchar(150)");

            builder.Property(c => c.DataPublicacao)
              .IsRequired()
              .HasColumnType("DateTime");

            builder.Property(c => c.Processo)
              .IsRequired()
              .HasColumnType("varchar(15)");

            builder.Property(c => c.Assunto)
              .IsRequired()
              .HasColumnType("nvarchar(150)");

            builder.Property(c => c.Entidade)
            .IsRequired()
            .HasColumnType("nvarchar(500)");

            builder.Property(c => c.CNPJ)
              .HasColumnType("varchar(18)");

            builder.Property(c => c.Tipo)
             .IsRequired()
             .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Descricao)
             .IsRequired()
             .HasColumnType("nvarchar(max)");

            builder.ToTable("Irregularidades");
        }
    }
}
