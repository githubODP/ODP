using Domain.Internos.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Internos.Mapping
{
    public class DemandasInternasMapping : IEntityTypeConfiguration<DemandasInternas>
    {
        public void Configure(EntityTypeBuilder<DemandasInternas> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Ano)
             .HasColumnType("int");

            builder.Property(c => c.Mes)
             .HasColumnType("int");

            builder.Property(c => c.NomeDocto)
               .HasColumnType("varchar(300)");

            builder.Property(c => c.Protocolo)
               .HasColumnType("varchar(15)");

            builder.Property(c => c.Objeto)
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Solicitacao)
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Orgao)
               .HasColumnType("varchar(200)");

            builder.Property(c => c.DataRecebe)
               .HasColumnType("Datetime");

            builder.Property(c => c.DataResposta)
               .HasColumnType("Datetime");

            builder.Property(c => c.Solicitante)
              .HasColumnType("varchar(200)");

            builder.Property(c => c.NomeSolicita)
              .HasColumnType("varchar(200)");

            builder.Property(c => c.Observacao)
              .HasColumnType("nvarchar(max)");

            builder.ToTable("Demandas");

        }
    }
}
