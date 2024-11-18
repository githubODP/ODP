
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

            builder.Property(c => c.DataDispensa)               
               .HasColumnType("Datetime");

            builder.Property(c => c.Fornecedor)            
                .HasColumnType("varchar(170)");

            builder.Property(c => c.CNPJ)                
                .HasColumnType("varchar(18)");

            builder.Property(c => c.CPF)               
               .HasColumnType("varchar(14)");


            builder.Property(c => c.Item)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.ValorDispensa)              
                .HasColumnType("float(30)");

            builder.Property(c => c.ValorItem)            
                .HasColumnType("float(30)");

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
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Protocolo)              
              .HasColumnType("varchar(15)");

            builder.Property(c => c.QuantidadeXValor)               
               .HasColumnType("float(30)");

            builder.ToTable("Dispensas");
        }
    }
}
