using Domain.GovernoFederal.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.GovernoFederal.Mapping
{
    public class AcordoLenienciaMapping : IEntityTypeConfiguration<AcordoLeniencia>
    {
        public void Configure(EntityTypeBuilder<AcordoLeniencia> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.IdentificacaoAcordo)
               .IsRequired()
               .HasColumnType("varchar(10)");

            builder.Property(c => c.CNPJ)
               .IsRequired()
               .HasColumnType("varchar(18)");

            builder.Property(c => c.RazaoSocial)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(c => c.DataInicioAcordo)
               .IsRequired()
               .HasColumnType("DateTime");

            builder.Property(c => c.DataFimAcordo)
               .IsRequired(false)
               .HasColumnType("DateTime");

            builder.Property(c => c.Situacao)
               .IsRequired()
               .HasColumnType("varchar(15)");

            builder.Property(c => c.NumeroProcesso)
               .IsRequired()
               .HasColumnType("varchar(40)");

            builder.Property(c => c.TermosAcordo)
               .IsRequired()
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.OrgaoSancionador)
               .IsRequired()
               .HasColumnType("varchar(60)");

            builder.Property(c => c.Efeitos)
               .IsRequired()
               .HasColumnType("varchar(110)");

            builder.Property(c => c.Complementos)
               .IsRequired(false)
               .HasColumnType("nvarchar(max)");



            builder.ToTable("AcordosLeniencia");

        }
    }
}
