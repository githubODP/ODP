
using Domain.GovernoFederal.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Infra.GovernoFederal.Mapping
{
    public class SeguroDefesoMapping : IEntityTypeConfiguration<SeguroDefeso>
    {
        public void Configure(EntityTypeBuilder<SeguroDefeso> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Ano)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.Mes)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.CodigoMunicipio)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.NomeMunicipio)
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(c => c.UF)
               .IsRequired()
               .HasColumnType("varchar(2)");

            builder.Property(c => c.CPF)
               .HasColumnType("varchar(15)");

            builder.Property(c => c.NISFavorecido)
               .IsRequired()
               .HasColumnType("varchar(15)");

            builder.Property(c => c.RGPFavorecido)
               .IsRequired()
               .HasColumnType("varchar(15)");

            builder.Property(c => c.NomeFavorecido)
               .IsRequired()
               .HasColumnType("varchar(300)");

            builder.Property(c => c.ValorParcela)
               .IsRequired()
               .HasColumnType("float");

            builder.ToTable("SeguroDefeso");
        }
    }
}
