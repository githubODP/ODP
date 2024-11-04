
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
               .IsRequired()
               .HasColumnType("varchar(200)");


            builder.Property(c => c.CNPJCPF)
              .IsRequired(false)
              .HasColumnType("varchar(18)");

            builder.Property(c => c.Protocolo)
               .IsRequired()
               .HasColumnType("varchar(15)");

            builder.Property(c => c.NumeroContrato)
               .IsRequired()
               .HasColumnType("varchar(30)");

            builder.Property(c => c.TipoContrato)
               .IsRequired()
               .HasColumnType("varchar(55)");

            builder.Property(c => c.Fiscal)
                .IsRequired()
                .HasColumnType("varchar(200)");


            builder.Property(c => c.TermoAditivo)
               .IsRequired()
               .HasColumnType("varchar(15)");

            builder.Property(c => c.Empenho)
              .IsRequired(false)
              .HasColumnType("varchar(20)");

            builder.Property(c => c.AnoEmpenho)
                .IsRequired(false)
                .HasColumnType("int");

            builder.Property(c => c.Liquidacao)
                .IsRequired(false)
             .HasColumnType("varchar(20)");

            builder.Property(c => c.DTLiquidacao)
                .IsRequired(false)
             .HasColumnType("Datetime");

            builder.Property(c => c.DTPagamento)
               .IsRequired(false)
            .HasColumnType("Datetime");

            builder.Property(c => c.QtdeAditivo)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.VlrTotalOriginal)
               .IsRequired()
               .HasColumnType("float(20)");

            builder.Property(c => c.VlrTotalAtual)
               .IsRequired()
               .HasColumnType("float(20)");

            builder.Property(c => c.ObjetoContrato)
              .IsRequired()
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Justificativa)
              .IsRequired(false)
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.DTInicioVigencia)
              .IsRequired(false)
              .HasColumnType("Datetime");

            builder.Property(c => c.DTFimVigencia)
              .IsRequired(false)
              .HasColumnType("Datetime");

            builder.ToTable("Contratos");
        }
    }
}
