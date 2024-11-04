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
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.CNPJCPF)
            .IsRequired()
            .HasColumnType("varchar(15)");

            builder.Property(c => c.Credor)
              .IsRequired()
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Processo)
              .IsRequired()
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Decisao)
             .IsRequired()
             .HasColumnType("nvarchar(max)");

            builder.Property(c => c.TipoSancao)
             .IsRequired()
             .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Referencia)
             .IsRequired()
             .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Valor)
             .IsRequired()
             .HasColumnType("varchar(20)");

            builder.Property(c => c.ValorRecolhido)
             .IsRequired()
             .HasColumnType("varchar(20)");

            builder.Property(c => c.SaldoDevedor)
             .IsRequired()
             .HasColumnType("varchar(20)");

            builder.Property(c => c.Execução)
           .IsRequired()
           .HasColumnType("nvarchar(max)");

            builder.ToTable("Inadimplentes");
        }
    }
}
