using Domain.Tribunal.Entidades.TSE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TSE
{
    public class FornecedorPartidoMapping : IEntityTypeConfiguration<FornecedorPartido>
    {
        public void Configure(EntityTypeBuilder<FornecedorPartido> builder)
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

            builder.Property(c => c.CNPJPartido)
             .IsRequired()
             .HasColumnType("varchar(18)");

            builder.Property(c => c.SiglaPartido)
             .IsRequired()
             .HasColumnType("varchar(15)");

            builder.Property(c => c.Partido)
             .IsRequired()
             .HasColumnType("varchar(50)");

            builder.Property(c => c.CNPJCPF)
            .IsRequired()
            .HasColumnType("varchar(18)");

            builder.Property(c => c.Fornecedor)
            .IsRequired()
            .HasColumnType("varchar(150)");

            builder.Property(c => c.DescricaoCargoFornecedor)
            .IsRequired()
            .HasColumnType("varchar(20)");

            builder.Property(c => c.DescricaoDespesas)
           .IsRequired()
           .HasColumnType("varchar(50)");

            builder.Property(c => c.ValorDespesas)
           .IsRequired()
           .HasColumnType("float");




            builder.ToTable("FornecedorPartidos");
        }
    }
}
