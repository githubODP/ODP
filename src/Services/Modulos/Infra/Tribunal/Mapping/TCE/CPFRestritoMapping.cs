using Domain.Tribunal.Entidades.TCE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TCE
{
    public class CPFRestritoMapping : IEntityTypeConfiguration<CPFRestrito>
    {
        public void Configure(EntityTypeBuilder<CPFRestrito> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Municipio)
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.CPF)
              .IsRequired()
              .HasColumnType("varchar(15)");

            builder.Property(c => c.NomeRazaoSocial)
             .IsRequired()
             .HasColumnType("nvarchar(max)");

            builder.Property(c => c.DataInicio)
             .HasColumnType("DateTime");

            builder.Property(c => c.DataFim)
             .HasColumnType("DateTime");

            builder.Property(c => c.TipoSancao)
             .IsRequired()
             .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Situacao)
             .IsRequired()
             .HasColumnType("varchar(10)");

            builder.ToTable("CPFRestritos");
        }
    }
}
