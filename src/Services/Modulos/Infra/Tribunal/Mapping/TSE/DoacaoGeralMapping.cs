﻿using Domain.Tribunal.Entidades.TSE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TSE
{
    public class DoacaoGeralMapping : IEntityTypeConfiguration<DoacaoGeral>
    {
        public void Configure(EntityTypeBuilder<DoacaoGeral> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.AnoEleicao)
             .IsRequired()
             .HasColumnType("int");

            builder.Property(c => c.IDPrestadorContas)
             .IsRequired()
             .HasColumnType("varchar(10)");

            builder.Property(c => c.UF)
             .IsRequired()
             .HasColumnType("varchar(2)");

            builder.Property(c => c.CPF)
             .IsRequired()
             .HasColumnType("varchar(14)");

            builder.Property(c => c.Nome)
             .IsRequired()
             .HasColumnType("varchar(70)");

            builder.Property(c => c.DataDoacao)
             .IsRequired()
             .HasColumnType("Datetime");

            builder.Property(c => c.DescricaoReceita)
             .IsRequired()
             .HasColumnType("nvarchar(50)");

            builder.Property(c => c.ValorDoacao)
             .IsRequired()
             .HasColumnType("float");

            builder.Property(c => c.CNPJPartido)
            .IsRequired()
            .HasColumnType("varchar(18)");

            builder.Property(c => c.NomePartido)
            .IsRequired()
            .HasColumnType("varchar(50)");

            builder.Property(c => c.SiglaPartido)
            .IsRequired()
            .HasColumnType("varchar(15)");




            builder.ToTable("DoacoesGerais");
        }
    }
}