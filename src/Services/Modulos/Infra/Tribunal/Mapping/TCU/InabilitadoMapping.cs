using Domain.Tribunal.Entidades.TCU;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TCU
{
    public class InabilitadoMapping : IEntityTypeConfiguration<Inabilitado>
    {
        public void Configure(EntityTypeBuilder<Inabilitado> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(100)");

            builder.Property(c => c.CPF)
            .IsRequired()
            .HasColumnType("varchar(15)");

            builder.Property(c => c.Processo)
            .IsRequired()
            .HasColumnType("varchar(15)");

            builder.Property(c => c.Deliberacao)
            .IsRequired()
            .HasColumnType("varchar(20)");

            builder.Property(c => c.TransitoJulgado)
            .HasColumnType("DateTime");

            builder.Property(c => c.DataFinal)
            .IsRequired()
            .HasColumnType("DateTime");

            builder.Property(c => c.DataAcordao)
            .IsRequired()
            .HasColumnType("DateTime");

            builder.ToTable("Inabilitados");
        }
    }
}
