using Dividas.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dividas.Infra.Mapping
{
    public class DividaFGTSMapping : IEntityTypeConfiguration<DividaFGTS>
    {
        public void Configure(EntityTypeBuilder<DividaFGTS> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CNPJCPF)
                .IsRequired()
                .HasColumnType("varchar(18)");

            builder.Property(c => c.TipoPessoa)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.TipoDevedor)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.NomeDevedor)
                .IsRequired()
                .HasColumnType("varchar(160)");

            builder.Property(c => c.UFUnidadeResponsavel)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.Property(c => c.UnidadeResponsavel)
                .IsRequired()
                .HasColumnType("varchar(25)");

            builder.Property(c => c.EntidadeResponsavel)
                .IsRequired()
                .HasColumnType("varchar(5)");

            builder.Property(c => c.UnidadeInscricao)
                .IsRequired()
                .HasColumnType("varchar(25)");

            builder.Property(c => c.NumeroInscricao)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(c => c.TipoSituacaoInscricao)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.SituacaoInscricao)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(c => c.ReceitaPrincipal)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(c => c.DataInscricao)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.IndicadorAjuizado)
                .IsRequired()
                .HasColumnType("varchar(3)");

            builder.Property(c => c.ValorConsolidado)
                .IsRequired()
                .HasColumnType("float");



            builder.ToTable("DividasFGTS");
        }
    }
}
