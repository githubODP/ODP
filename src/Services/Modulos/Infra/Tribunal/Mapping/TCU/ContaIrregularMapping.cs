using Domain.Tribunal.Entidades.TCU;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TCU
{
    public class ContaIrregularMapping : IEntityTypeConfiguration<ContaIrregular>
    {
        public void Configure(EntityTypeBuilder<ContaIrregular> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Nome)
              .IsRequired()
              .HasColumnType("varchar(150)");


            builder.Property(c => c.CNPJCPF)
             .IsRequired()
             .HasColumnType("varchar(18)");

            builder.Property(c => c.Processo)
             .IsRequired()
             .HasColumnType("varchar(18)");

            builder.Property(c => c.Municipio)
             .IsRequired()
             .HasColumnType("varchar(100)");

            builder.Property(c => c.UF)
             .IsRequired()
             .HasColumnType("varchar(02)");

            builder.Property(c => c.Deliberacao)
             .IsRequired()
             .HasColumnType("varchar(18)");

            builder.Property(c => c.DataJulgado)
             .IsRequired()
             .HasColumnType("DateTime");

            builder.ToTable("ContasIrregulares");
        }
    }
}
