﻿
using Domain.Corregedoria.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Corregedoria.Mapping
{
    public class InstauracaoMapping : IEntityTypeConfiguration<Instauracao>
    {


        public void Configure(EntityTypeBuilder<Instauracao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Ano)

                .HasColumnType("int");

            builder.Property(c => c.CNPJCPF)

                .HasColumnType("varchar(20)");

            builder.Property(c => c.RG)

                .HasColumnType("varchar(100)");

            builder.Property(c => c.Orgao)

                .HasColumnType("varchar(50)");

            builder.Property(c => c.Procedimento)

                .HasColumnType("varchar(30)");

            builder.Property(c => c.Protocolo)

                .HasColumnType("varchar(70)");

            builder.Property(c => c.Objeto)

               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.AtoNormativo)

               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.DataPublicacao)
                .HasColumnType("Datetime");

            builder.Property(c => c.NumeroDIOE)
                .HasColumnType("int");

            builder.Property(c => c.AtoNormativoDecisao)

                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.NumeroDIOEDecisao)

                .HasColumnType("int");

            builder.Property(c => c.DataPublicacaoDecisao)
                .HasColumnType("Datetime");

            builder.Property(c => c.Decisao)

                .HasColumnType("varchar(150)");

            builder.Property(c => c.InfoAdd)

              .HasColumnType("nvarchar(max)");

            builder.ToTable("Instauracoes");
        }

    }
}