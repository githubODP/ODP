using Domain.GovernoEstadual.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.GovernoEstadual.Mapping
{
    public class PADVMapping : IEntityTypeConfiguration<PADV>
    {
        public void Configure(EntityTypeBuilder<PADV> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Agencia)
               .IsRequired()
               .HasColumnType("varchar(50)");

            builder.Property(c => c.Ano)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.Mes)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.ValorBruto)
               .IsRequired()
               .HasColumnType("float");

            builder.Property(c => c.ValorPago)
               .IsRequired()
               .HasColumnType("float");

            builder.Property(c => c.RazaoSocial)
              .IsRequired()
              .HasColumnType("varchar(150)");

            builder.Property(c => c.NomeFantasia)
               .IsRequired(false)
               .HasColumnType("varchar(80)");

            builder.Property(c => c.Cidade)
               .IsRequired()
               .HasColumnType("varchar(30)");

            builder.Property(c => c.NumeroPADV)
               .IsRequired()
               .HasColumnType("varchar(15)");

            builder.Property(c => c.OrgaoSolicitante)
               .IsRequired()
               .HasColumnType("varchar(20)");

            builder.Property(c => c.CNPJExecutor)
              .IsRequired(false)
              .HasColumnType("varchar(18)");

            builder.Property(c => c.OrgaoPagador)
               .IsRequired()
               .HasColumnType("varchar(20)");

            builder.Property(c => c.Objetivo)
               .IsRequired()
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.SituacaoFornecedor)
               .IsRequired()
               .HasColumnType("varchar(50)");

            builder.Property(c => c.SituacaoPADV)
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.ToTable("PADVs");

        }
    }
}
