using Domain.Fazenda.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ODP.Fazenda.API.Models.Mapping
{

    public class NFEletronicaMapping : IEntityTypeConfiguration<NFEletronica>
    {
        public void Configure(EntityTypeBuilder<NFEletronica> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.NroReceitaEstadual)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.CodigoUF)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.CodigoNF)
                .HasColumnType("int");

            builder.Property(c => c.NaturezaOperacao)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(c => c.ModeloNF)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.SerieNF)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.NumeroNF)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.DataEmissao)
               .IsRequired()
               .HasColumnType("DateTime");

            builder.Property(c => c.Mes)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Ano)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.TipoNF)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.CNPJ)
                .IsRequired(false)
                .HasColumnType("varchar(18)");

            builder.Property(c => c.Emitente)
                .IsRequired()
                .HasColumnType("varchar(70)");

            builder.Property(c => c.EnderecoEmitente)
                .IsRequired()
                .HasColumnType("varchar(70)");

            builder.Property(c => c.NroEmitente)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(c => c.BairroEmitente)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(c => c.MunicipioEmitente)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.UFEmitente)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.Property(c => c.CEPEmitente)
                .IsRequired()
                .HasColumnType("varchar(9)");

            builder.Property(c => c.CNPJDestinatario)
                .IsRequired()
                .HasColumnType("varchar(18)");

            builder.Property(c => c.Destinatario)
                .IsRequired()
                .HasColumnType("varchar(70)");

            builder.Property(c => c.EnderecoDestinatario)
               .IsRequired()
               .HasColumnType("varchar(70)");

            builder.Property(c => c.NroDestinatario)
               .IsRequired()
               .HasColumnType("varchar(30)");


            builder.Property(c => c.BairroDestinatario)
               .IsRequired()
               .HasColumnType("varchar(60)");


            builder.Property(c => c.MunicipioDestinatario)
               .IsRequired()
               .HasColumnType("varchar(50)");

            builder.Property(c => c.UFDestinatario)
               .IsRequired()
               .HasColumnType("varchar(2)");

            builder.Property(c => c.CEPDestinatario)
               .HasColumnType("varchar(9)");

            builder.Property(c => c.CodigoProduto)
               .IsRequired()
               .HasColumnType("varchar(50)");

            builder.Property(c => c.Produto)
              .IsRequired()
              .HasColumnType("Nvarchar(300)");


            builder.Property(c => c.UnidadeProduto)
              .IsRequired()
              .HasColumnType("varchar(10)");

            builder.Property(c => c.QuantidadeProduto)
              .IsRequired()
              .HasColumnType("float");

            builder.Property(c => c.VlrUnitarioProduto)
             .IsRequired()
             .HasColumnType("float");


            builder.Property(c => c.ValorProduto)
             .IsRequired()
             .HasColumnType("float");

            builder.Property(c => c.ValorNotaFiscal)
              .IsRequired()
              .HasColumnType("float");
            

            builder.Property(c => c.InformacoesAdicionais)
              .IsRequired(false)
               .HasColumnType("nvarchar(max)");

            builder.ToTable("NFEletronicas");
        }
    }

}
