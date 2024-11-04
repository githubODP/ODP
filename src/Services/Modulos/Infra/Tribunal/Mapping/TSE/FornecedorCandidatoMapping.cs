using Domain.Tribunal.Entidades.TSE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TSE
{
    public class FornecedorCandidatoMapping : IEntityTypeConfiguration<FornecedorCandidato>
    {
        public void Configure(EntityTypeBuilder<FornecedorCandidato> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.AnoEleicao)
             .IsRequired()
             .HasColumnType("int");

            builder.Property(c => c.UF)
             .IsRequired()
             .HasColumnType("varchar(2)");

            builder.Property(c => c.Municipio)
             .IsRequired()
             .HasColumnType("varchar(30)");

            builder.Property(c => c.Candidato)
            .IsRequired()
            .HasColumnType("varchar(100)");

            builder.Property(c => c.CPFCandidato)
             .IsRequired()
             .HasColumnType("varchar(14)");

            builder.Property(c => c.DescricaoCargo)
             .IsRequired()
             .HasColumnType("varchar(50)");

            builder.Property(c => c.Partido)
             .IsRequired()
             .HasColumnType("varchar(50)");

            builder.Property(c => c.CNPJCPF)
            .IsRequired()
            .HasColumnType("varchar(18)");

            builder.Property(c => c.Fornecedor)
            .IsRequired()
            .HasColumnType("varchar(150)");

            builder.Property(c => c.DescricaoTipoDocto)
            .IsRequired()
            .HasColumnType("varchar(40)");

            builder.Property(c => c.DescricaoDespesas)
           .IsRequired()
           .HasColumnType("varchar(90)");

            builder.Property(c => c.ValorDespesas)
           .IsRequired()
           .HasColumnType("float");




            builder.ToTable("FornecedorCandidatos");
        }
    }
}
