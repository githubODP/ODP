
using Domain.RecursosHumanos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.RecursosHumanos.Mapping
{
    public class RubricaMapping : IEntityTypeConfiguration<Rubrica>
    {
        public void Configure(EntityTypeBuilder<Rubrica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Ano)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Mes)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Matricula)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Ordinal)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(c => c.Orgao)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.DescricaoRubrica)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(c => c.TipoRubrica)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Valor)
                .IsRequired()
                .HasColumnType("Decimal(15)");

            builder.Property(c => c.IDUnico)
                .IsRequired()
                .HasColumnType("varchar(10)");



            builder.ToTable("Rubricas");
        }
    }

}
