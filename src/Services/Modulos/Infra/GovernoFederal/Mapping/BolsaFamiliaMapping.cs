using Domain.GovernoFederal.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.GovernoFederal.Mapping
{
    public class BolsaFamiliaMapping : IEntityTypeConfiguration<BolsaFamilia>
    {
        public void Configure(EntityTypeBuilder<BolsaFamilia> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CodigoMunicipioSIAFI)
               .IsRequired()
               .HasColumnType("varchar(5)");

            builder.Property(c => c.NomeMunicipio)
               .IsRequired()
               .HasColumnType("varchar(50)");

            builder.Property(c => c.UF)
               .IsRequired()
               .HasColumnType("varchar(2)");

            builder.Property(c => c.Nome)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(c => c.CPF)
              .IsRequired(false)
              .HasColumnType("varchar(15)");

            builder.Property(c => c.Nis)
               .IsRequired()
               .HasColumnType("varchar(15)");

            builder.Property(c => c.AnoCompetencia)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.AnoReferencia)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.MesCompetencia)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.MesReferencia)
               .IsRequired()
               .HasColumnType("int");     

            builder.Property(c => c.Valor)
               .IsRequired()
               .HasColumnType("float");

            builder.ToTable("BolsaFamilia");
        }
    }
}
