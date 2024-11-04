using Domain.GovernoEstadual.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.GovernoEstadual.Mapping
{
    public class AmbientalMapping : IEntityTypeConfiguration<Ambiental>
    {
        public void Configure(EntityTypeBuilder<Ambiental> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Municipio)
               .IsRequired()
               .HasColumnType("varchar(30)");

            builder.Property(c => c.CNPJCPF)
               .IsRequired(false)
               .HasColumnType("varchar(18)");

            builder.Property(c => c.Infrator)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(c => c.QtdeInfracoes)
               .IsRequired()
               .HasColumnType("varchar(2)");

            builder.Property(c => c.Situacao)
               .IsRequired()
               .HasColumnType("varchar(25)");

            builder.Property(c => c.DTInscricaoSEFA)
              .IsRequired(false)
              .HasColumnType("Datetime");

            builder.Property(c => c.AnoInfracao)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.ValorAutuacao)
               .IsRequired()
               .HasColumnType("float");

            builder.Property(c => c.ValorOficioPago)
               .IsRequired()
               .HasColumnType("float");

            builder.Property(c => c.Infracao)
               .IsRequired()
               .HasColumnType("int");

            builder.ToTable("Ambientais");

        }
    }
}
