using Domain.Internos.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Internos.Mapping
{
    public class ContratosInternoMapping : IEntityTypeConfiguration<ContratosInternos>
    {
        public void Configure(EntityTypeBuilder<ContratosInternos> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Contrato)
               .HasColumnType("varchar(300)");

            builder.Property(c => c.NroContrato)
               .HasColumnType("varchar(20)");

            builder.Property(c => c.InicioContrato)
               .HasColumnType("Datetime");

            builder.Property(c => c.FimContrato)
               .HasColumnType("Datetime");

            builder.Property(c => c.Protocolo)
              .HasColumnType("varchar(15)");

            builder.Property(c => c.Valor)
              .HasColumnType("float");

            builder.Property(c => c.Objeto)
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Gestor)
              .HasColumnType("varchar(300)");

            builder.Property(c => c.Fiscal)
              .HasColumnType("varchar(300)");

            builder.Property(c => c.Dioe)
             .HasColumnType("int");

            builder.Property(c => c.DataPublicacao)
               .HasColumnType("Datetime");

            builder.Property(c => c.Status)
              .HasColumnType("varchar(30)");

            builder.ToTable("ContratosInternos");



        }
    }
}
