
using Domain.Compras.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Compras.Mapping
{
    public class LicitacaoMapping : IEntityTypeConfiguration<Licitacao>
    {
        public void Configure(EntityTypeBuilder<Licitacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Protocolo)
                .IsRequired(false)
                .HasColumnType("varchar(15)");

            builder.Property(c => c.Orgao)
                .IsRequired()
                .HasColumnType("varchar(17)");

            builder.Property(c => c.Ano)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Mes)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Fornecedor)
                .IsRequired(false)
                .HasColumnType("varchar(300)");

            builder.Property(c => c.CNPJCPF)
                .IsRequired(false)
                .HasColumnType("varchar(18)");

            builder.Property(c => c.Modalidade)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Situacao)
                .IsRequired()
                .HasColumnType("varchar(40)");

            builder.Property(c => c.Objeto)
                .IsRequired(false)
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.ValorEstimado)

                .HasColumnType("float");

            builder.Property(c => c.ValorLicitado)
               .HasColumnType("float");

            builder.Property(c => c.ValorHomologado)
                .HasColumnType("float");

            builder.ToTable("Licitacoes");
        }
    }
}
