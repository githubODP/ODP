using Domain.Fazenda.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Fazenda.Mapping
{
    public class NFEletronicaFederalMapping : IEntityTypeConfiguration<NFEletronicaFederal>
    {
        public void Configure(EntityTypeBuilder<NFEletronicaFederal> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Serie)
                .IsRequired()
                .HasColumnType("varchar(05)");

            builder.Property(c => c.NumeroNF)
            .IsRequired()
            .HasColumnType("varchar(15)");


            builder.Property(c => c.CNPJ)

            .HasColumnType("varchar(18)");


            builder.Property(c => c.CPF)

            .HasColumnType("varchar(14)");


            builder.Property(c => c.Emitente)
            .IsRequired()
            .HasColumnType("varchar(300)");

            builder.Property(c => c.UFEmitente)
           .IsRequired()
           .HasColumnType("varchar(02)");

            builder.Property(c => c.MunicipioEmitente)
            .IsRequired()
            .HasColumnType("varchar(200)");

            builder.Property(c => c.Destinatario)
            .IsRequired()
            .HasColumnType("varchar(300)");


            builder.Property(c => c.CNPJDestinatario)
            .IsRequired()
            .HasColumnType("varchar(18)");

            builder.Property(c => c.UFDestinatario)
            .IsRequired()
            .HasColumnType("varchar(02)");

            builder.Property(c => c.ValorNF)
            .IsRequired()
            .HasColumnType("float");

            builder.Property(c => c.ChaveAcesso)
           .IsRequired()
           .HasColumnType("varchar(45)");

            builder.Property(c => c.DescricaoProdutoServico)
           .IsRequired()
           .HasColumnType("nvarchar(200)");

            builder.Property(c => c.Codigo)
           .IsRequired()
           .HasColumnType("varchar(15)");

            builder.Property(c => c.TipoProduto)
           .HasColumnType("nvarchar(300)");

            builder.Property(c => c.Quantidade)
           .IsRequired()
           .HasColumnType("int");

            builder.Property(c => c.Unidade)
           .HasColumnType("varchar(15)");


            builder.Property(c => c.ValorUnitario)
           .IsRequired()
           .HasColumnType("float");


            builder.Property(c => c.ValorTotal)
           .IsRequired()
           .HasColumnType("float");


            builder.Property(c => c.DataEmissao)
            .IsRequired()
            .HasColumnType("DateTime");



            builder.ToTable("NFEletronicasFederal");
        }
    }
}