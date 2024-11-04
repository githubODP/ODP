using Domain.GovernoFederal.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.GovernoFederal.Mapping
{
    public class PEPMapping : IEntityTypeConfiguration<PEP>
    {
        public void Configure(EntityTypeBuilder<PEP> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Nome)
               .IsRequired()
               .HasColumnType("varchar(300)");

            builder.Property(c => c.CPF)
               .HasColumnType("varchar(18)");

            builder.Property(c => c.Orgao)
               .IsRequired()
               .HasColumnType("varchar(500)");

            builder.Property(c => c.DataFim)
               .HasColumnType("DateTime");

            builder.Property(c => c.DataFimCarencia)
               .HasColumnType("DateTime");

            builder.Property(c => c.DataInicio)
               .HasColumnType("DateTime");

            builder.Property(c => c.Funcao)
               .HasColumnType("varchar(100)");



            builder.ToTable("PEPS");
        }
    }
}
