using Domain.Tribunal.Entidades.TCE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TCE
{
    public class CNPJRestritoMapping : IEntityTypeConfiguration<CNPJRestrito>
    {
        public void Configure(EntityTypeBuilder<CNPJRestrito> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Municipio)
              .IsRequired()
              .HasColumnType("varchar(50)");

            builder.Property(c => c.CNPJ)
              .IsRequired()
              .HasColumnType("varchar(18)");

            builder.Property(c => c.NomeRazaoSocial)
             .IsRequired()
             .HasColumnType("nvarchar(max)");

            builder.Property(c => c.DataInicio)
             .HasColumnType("DateTime");

            builder.Property(c => c.DataFim)
             .HasColumnType("DateTime");

            builder.Property(c => c.TipoSancao)
             .IsRequired()
             .HasColumnType("varchar(50)");

            builder.Property(c => c.Situacao)
             .IsRequired()
             .HasColumnType("nvarchar(max)");

            builder.ToTable("CNPJRestritos");
        }
    }
}