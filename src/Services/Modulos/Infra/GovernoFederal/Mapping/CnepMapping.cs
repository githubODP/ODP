using Domain.GovernoFederal.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.GovernoFederal.Mapping
{
    public class CnepMapping : IEntityTypeConfiguration<Cnep>
    {
        public void Configure(EntityTypeBuilder<Cnep> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.RazaoSocial)
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(c => c.CNPJCPF)
               .IsRequired()
               .HasColumnType("varchar(18)");

            builder.Property(c => c.CodigoSancao)
               .IsRequired()
               .HasColumnType("varchar(10)");

            builder.Property(c => c.NomeFantasia)
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(c => c.NroProcesso)
               .IsRequired(false)
                 .HasColumnType("varchar(50)");

            builder.Property(c => c.CategoriaSancao)
               .IsRequired()
               .HasColumnType("varchar(80)");

            builder.Property(c => c.ValorMulta)
               .IsRequired()
               .HasColumnType("float");

            builder.Property(c => c.DataInicioSancao)
               .IsRequired()
               .HasColumnType("DateTime");

            builder.Property(c => c.DataFimSancao)
               .IsRequired()
               .HasColumnType("DateTime");

            builder.Property(c => c.DataPublicacao)
               .IsRequired()
               .HasColumnType("DateTime");

            builder.Property(c => c.Detalhamento)
               .IsRequired(false)
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.DataTransitoJulgado)
              .IsRequired(false)
              .HasColumnType("DateTime");

            builder.Property(c => c.Abrangencia)
              .IsRequired(false)
              .HasColumnType("varchar(50)");

            builder.Property(c => c.OrgaoSancionador)
              .IsRequired(false)
              .HasColumnType("varchar(110)");

            builder.Property(c => c.EsferaOrgao)
              .IsRequired(false)
              .HasColumnType("varchar(10)");

            builder.Property(c => c.FundamentacaoLegal)
              .IsRequired(false)
              .HasColumnType("nvarchar(max)");



            builder.ToTable("CNEP");

        }
    
        
    }
}

