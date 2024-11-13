using Dividas.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Dividas.Infra.Mapping
{
    public class DividaNaoPrevidenciaMapping : IEntityTypeConfiguration<DividaNaoPrevidenciaria>
    {
        public void Configure(EntityTypeBuilder<DividaNaoPrevidenciaria> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CNPJ)
                .IsRequired()
                .HasColumnType("varchar(18)");


            builder.Property(c => c.CPF)
               .IsRequired()
               .HasColumnType("varchar(14)");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.TipoPessoa)
                .IsRequired()
                .HasColumnType("varchar(16)");

            builder.Property(c => c.TipoDevedor)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(c => c.NomeDevedor)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(c => c.UFUnidadeResponsavel)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.Property(c => c.UnidadeResponsavel)
                .IsRequired()
                .HasColumnType("varchar(25)");

            builder.Property(c => c.NumeroInscricao)
                .IsRequired()
                .HasColumnType("varchar(25)");

            builder.Property(c => c.TipoSituacaoInscricao)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(c => c.SituacaoInscricao)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(c => c.ReceitaPrincipal)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.DataInscricao)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.IndicadorAjuizado)
                .IsRequired()
                .HasColumnType("varchar(3)");

            builder.Property(c => c.ValorConsolidado)
                .IsRequired()
                .HasColumnType("float");

            builder.ToTable("DividasNaoPrevidenciarias");
        }
    }
}
