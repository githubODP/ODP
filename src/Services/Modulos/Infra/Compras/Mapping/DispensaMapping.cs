
using Domain.Compras.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Compras.Mapping
{
    public class DispensaMapping : IEntityTypeConfiguration<Dispensa>
    {
        public void Configure(EntityTypeBuilder<Dispensa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Ano)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Mes)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Solicitacao)
                .IsRequired()
                .HasColumnType("varchar(7)");

            builder.Property(c => c.Fornecedor)
                .IsRequired()
                .HasColumnType("varchar(170)");

            builder.Property(c => c.CNPJCPF)
                .IsRequired()
                .HasColumnType("varchar(18)");

            builder.Property(c => c.Item)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.ValorDispensa)
                .IsRequired()
                .HasColumnType("float(20)");

            builder.Property(c => c.ValorItem)
                .IsRequired()
                .HasColumnType("float(20)");

            builder.Property(c => c.QtdItem)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.Orgao)
               .IsRequired()
               .HasColumnType("varchar(20)");

            builder.Property(c => c.CodigoItem)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.Modalidade)
               .IsRequired()
               .HasColumnType("varchar(150)");

            builder.Property(c => c.Objeto)
               .IsRequired()
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Protocolo)
               .IsRequired()
              .HasColumnType("varchar(15)");

            builder.Property(c => c.QuantidadeXValor)
               .IsRequired()
               .HasColumnType("float(20)");

            builder.ToTable("Dispensas");
        }
    }
}
