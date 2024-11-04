
using Domain.GovernoFederal.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.GovernoFederal.Mapping
{
    public class ExpulsoFederalMapping : IEntityTypeConfiguration<ExpulsoFederal>
    {
        public void Configure(EntityTypeBuilder<ExpulsoFederal> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Esfera)
               .IsRequired()
               .HasColumnType("varchar(10)");

            builder.Property(c => c.Nome)
               .HasColumnType("varchar(100)");

            builder.Property(c => c.CPF)
               .IsRequired()
               .HasColumnType("varchar(18)");

            builder.Property(c => c.DataPublicacao)
               .HasColumnType("DateTime");

            builder.Property(c => c.Punicao)
               .IsRequired()
               .HasColumnType("varchar(600)");

            builder.Property(c => c.NumeroPortaria)
               .HasColumnType("varchar(100)");


            builder.Property(c => c.SecaoExtraDOU)
               .IsRequired()
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.NumeroProcesso)
               .HasColumnType("varchar(30)");

            builder.Property(c => c.Unidade)
               .IsRequired()
               .HasColumnType("varchar(500)");

            builder.Property(c => c.UF)
               .HasColumnType("varchar(2)");

            builder.Property(c => c.CargoEfetivo)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(c => c.CargoComissao)
               .IsRequired()
               .HasColumnType("varchar(500)");

            builder.Property(c => c.FundamentosSancao)
               .IsRequired()
               .HasColumnType("nvarchar(max)");



            builder.ToTable("ExpulsosFederais");
        }
    }
}
