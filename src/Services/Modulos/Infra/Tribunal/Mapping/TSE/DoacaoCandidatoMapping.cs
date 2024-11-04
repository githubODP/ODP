using Domain.Tribunal.Entidades.TSE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TSE
{
    public class DoacaoCandidatoMapping : IEntityTypeConfiguration<DoacaoCandidato>
    {
        public void Configure(EntityTypeBuilder<DoacaoCandidato> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.UF)
              .IsRequired()
              .HasColumnType("varchar(2)");

            builder.Property(c => c.Municipio)
              .IsRequired()
              .HasColumnType("varchar(30)");

            builder.Property(c => c.IDPrestadorContas)
             .IsRequired()
             .HasColumnType("varchar(10)");

            builder.Property(c => c.CNPJPrestadorConta)
             .IsRequired()
             .HasColumnType("varchar(18)");

            builder.Property(c => c.NomeCandidato)
             .IsRequired()
             .HasColumnType("varchar(70)");

            builder.Property(c => c.CPFCandidato)
             .IsRequired()
             .HasColumnType("varchar(14)");

            builder.Property(c => c.Cargo)
             .IsRequired()
             .HasColumnType("nvarchar(20)");


            builder.Property(c => c.SiglaPartido)
              .IsRequired()
              .HasColumnType("varchar(15)");

            builder.Property(c => c.NomePartido)
              .IsRequired()
              .HasColumnType("varchar(50)");

            builder.Property(c => c.DescricaoOrigemReceita)
             .IsRequired()
             .HasColumnType("nvarchar(50)");

            builder.Property(c => c.DescricaoEspecieReceita)
             .IsRequired()
             .HasColumnType("varchar(100)");

            builder.Property(c => c.NomeDoador)
             .IsRequired()
             .HasColumnType("varchar(150)");

            builder.Property(c => c.CPFDoador)
             .IsRequired()
             .HasColumnType("varchar(18)");

            builder.Property(c => c.NroReciboDoacao)
             .IsRequired()
             .HasColumnType("varchar(25)");

            builder.Property(c => c.NroDoctoDoacao)
             .IsRequired()
             .HasColumnType("varchar(40)");

            builder.Property(c => c.DataReceita)
             .IsRequired()
             .HasColumnType("DateTime");

            builder.Property(c => c.DescricaoReceita)
             .IsRequired()
             .HasColumnType("varchar(100)");

            builder.Property(c => c.ValorDoacao)
             .IsRequired()
             .HasColumnType("float");

            builder.Property(c => c.AnoEleicao)
             .IsRequired()
             .HasColumnType("int");

            builder.ToTable("DoacoesCandidatos");
        }
    }
}
