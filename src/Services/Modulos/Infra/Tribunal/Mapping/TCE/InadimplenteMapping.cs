using Domain.Tribunal.Entidades.TCE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TCE
{
    public class InadimplenteMapping : IEntityTypeConfiguration<Inadimplente>
    {
        public void Configure(EntityTypeBuilder<Inadimplente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Devedor)
              .IsRequired()
              .HasColumnType("varchar(300)");

            builder.Property(c => c.CNPJCPF)
            .IsRequired()
            .HasColumnType("varchar(18)");

            builder.Property(c => c.Credor)
              .IsRequired()
              .HasColumnType("varchar(100)");

            builder.Property(c => c.Processo)
              .IsRequired()
              .HasColumnType("varchar(10)");

            builder.Property(c => c.Decisao)
             .IsRequired()
             .HasColumnType("varchar(30)");

            builder.Property(c => c.TipoSancao)
             .IsRequired()
             .HasColumnType("varchar(30)");

            builder.Property(c => c.Referencia)
             .IsRequired()
             .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Valor)
             .IsRequired()
             .HasColumnType("float");

            builder.Property(c => c.ValorRecolhido)
             .IsRequired()
             .HasColumnType("float");

            builder.Property(c => c.SaldoDevedor)
             .IsRequired()
             .HasColumnType("float");

            builder.Property(c => c.Execução)
           .IsRequired()
           .HasColumnType("nvarchar(max)");

            builder.ToTable("Inadimplentes");
        }
    }
}
