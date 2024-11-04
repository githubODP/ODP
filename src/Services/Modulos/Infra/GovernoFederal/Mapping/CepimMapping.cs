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
    public class CepimMapping : IEntityTypeConfiguration<Cepim>
    {
        public void Configure(EntityTypeBuilder<Cepim> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CNPJ)
               .IsRequired()
               .HasColumnType("varchar(18)");

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.NroConvenio)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Orgao)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Impedimento)
                .IsRequired()
                .HasColumnType("varchar(50)");

           
            builder.ToTable("Cepim");
        }
    }
}
