
using Domain.GovernoFederal.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.GovernoFederal.Mapping
{
    public class BeneficioContinuoMapping : IEntityTypeConfiguration<BeneficioContinuo>
    {
        public void Configure(EntityTypeBuilder<BeneficioContinuo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Ano)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.MesCompetencia)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.MesReferencia)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.CodigoMunicipio)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.NomeMunicipio)
               .IsRequired()
               .HasColumnType("varchar(35)");

            builder.Property(c => c.UFMunicipio)
               .IsRequired()
               .HasColumnType("varchar(2)");

            builder.Property(c => c.NisBeneficiario)
               .IsRequired(false)
               .HasColumnType("varchar(12)");

            builder.Property(c => c.CPF)
               .IsRequired(false)
               .HasColumnType("varchar(15)");

            builder.Property(c => c.NomeBeneficiario)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(c => c.NISRepresentanteLegal)
               .IsRequired(false)
               .HasColumnType("varchar(12)");

            builder.Property(c => c.CPFRepresentanteLegal)
               .IsRequired(false)
               .HasColumnType("varchar(15)");

            builder.Property(c => c.NomeRepresentanteLegal)
               .IsRequired(false)
               .HasColumnType("varchar(100)");

            builder.Property(c => c.NumeroBeneficio)
               .IsRequired()
               .HasColumnType("varchar(12)");

            builder.Property(c => c.BeneficioConcessaoJudicial)
               .IsRequired()
               .HasColumnType("varchar(3)");

            builder.Property(c => c.ValorParcela)
               .IsRequired()
               .HasColumnType("float");

            builder.ToTable("BeneficiosContinuos");
        }
    }
}
