
using Domain.Compras.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Infra.Compras.Mapping
{
    public class ContratoMapping : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.AnoContrato)
            .IsRequired()
            .HasColumnType("int");

            builder.Property(c => c.OrgaoGestor)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.OrgaoGMS)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.StatusContrato)
                .IsRequired()
                .HasColumnType("varchar(25)");

            builder.Property(c => c.Fornecedor)
               .HasColumnType("varchar(200)");

            builder.Property(c => c.CNPJ)
              .HasColumnType("varchar(18)");

            builder.Property(c => c.CPF)
              .HasColumnType("varchar(14)");

            builder.Property(c => c.Protocolo)
               .HasColumnType("varchar(15)");

            builder.Property(c => c.NumeroContrato)
               .IsRequired()
               .HasColumnType("varchar(30)");

            builder.Property(c => c.TipoContrato)
               .IsRequired()
               .HasColumnType("varchar(55)");


            builder.Property(c => c.TermoAditivo)
               .HasColumnType("varchar(15)");

            builder.Property(c => c.Empenho)
              .HasColumnType("varchar(20)");

            builder.Property(c => c.QtdeAditivo)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.VlrTotalOriginal)

               .HasColumnType("float(30)");

            builder.Property(c => c.VlrTotalAtual)

               .HasColumnType("float(30)");

            builder.Property(c => c.ObjetoContrato)
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Justificativa)
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.DTInicioVigencia)
              .HasColumnType("Datetime");

            builder.Property(c => c.DTFimVigencia)
              .HasColumnType("Datetime");

            builder.ToTable("Contratos");
        }
    }
}
