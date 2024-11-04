using Domain.Tribunal.Entidades.TSE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TSE
{
    public class DoacaoPartidoMapping : IEntityTypeConfiguration<DoacaoPartido>
    {
        public void Configure(EntityTypeBuilder<DoacaoPartido> builder)
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

            builder.Property(c => c.Municipio)
             .IsRequired()
             .HasColumnType("varchar(30)");

            builder.Property(c => c.CNPJPrestadorConta)
             .IsRequired()
             .HasColumnType("varchar(18)");

            builder.Property(c => c.SiglaPartido)
              .IsRequired()
              .HasColumnType("varchar(15)");

            builder.Property(c => c.NomePartido)
              .IsRequired()
              .HasColumnType("varchar(50)");

            builder.Property(c => c.DescricaoFonteReceita)
             .IsRequired()
             .HasColumnType("nvarchar(50)");

            builder.Property(c => c.DescricaoOrigemReceita)
             .IsRequired()
             .HasColumnType("nvarchar(50)");

            builder.Property(c => c.DescricaoEspecieReceita)
             .IsRequired()
             .HasColumnType("nvarchar(100)");

            builder.Property(c => c.NomeDoador)
             .IsRequired()
             .HasColumnType("varchar(70)");

            builder.Property(c => c.CNPJCPFDoador)
             .IsRequired()
             .HasColumnType("varchar(18)");

            builder.Property(c => c.MunicipioDoador)
            .IsRequired()
            .HasColumnType("varchar(30)");

            builder.Property(c => c.DescricaoCandidatoDoador)
             .IsRequired()
             .HasColumnType("varchar(100)");

            builder.Property(c => c.SiglaPartido)
            .IsRequired()
            .HasColumnType("varchar(15)");

            builder.Property(c => c.NomePartidoDoador)
           .IsRequired()
           .HasColumnType("varchar(50)");

            builder.Property(c => c.DataReceita)
           .IsRequired()
           .HasColumnType("Datetime");

            builder.Property(c => c.DescricaoReceita)
           .IsRequired()
           .HasColumnType("varchar(100)");

            builder.Property(c => c.ValorReceita)
           .IsRequired()
           .HasColumnType("float");

            builder.ToTable("DoacoesPartidos");
        }
    }
}
