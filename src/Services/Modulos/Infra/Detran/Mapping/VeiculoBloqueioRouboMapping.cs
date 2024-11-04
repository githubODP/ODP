
using Domain.Detran.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.Detran.Mapping
{
    public class VeiculoBloqueioRouboMapping : IEntityTypeConfiguration<VeiculoBloqueioRoubo>
    {
        public void Configure(EntityTypeBuilder<VeiculoBloqueioRoubo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CNPJCPF)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(c => c.Proprietario)
            .IsRequired()
            .HasColumnType("varchar(170)");

            builder.Property(c => c.Situacao)
            .IsRequired()
            .HasColumnType("varchar(50)");

            builder.Property(c => c.Tipo)
            .IsRequired()
            .HasColumnType("varchar(17)");

            builder.Property(c => c.Placa)
            .IsRequired()
            .HasColumnType("varchar(8)");

            builder.Property(c => c.Renavan)
            .IsRequired()
            .HasColumnType("int");

            builder.Property(c => c.MarcaModelo)
            .IsRequired()
            .HasColumnType("varchar(25)");

            builder.Property(c => c.Cor)
            .IsRequired()
            .HasColumnType("varchar(10)");

            builder.Property(c => c.Fabricacao)
            .IsRequired()
            .HasColumnType("int");

            builder.Property(c => c.Modelo)
            .IsRequired()
            .HasColumnType("varchar(20)");

            builder.Property(c => c.Aquisicao)
            .IsRequired()
            .HasColumnType("DateTime");

            builder.Property(c => c.CNPJCPFAnterior)
            .IsRequired()
            .HasColumnType("varchar(15)");

            builder.Property(c => c.ProprietarioAnterior)
            .IsRequired()
            .HasColumnType("varchar(170)");

            builder.ToTable("VeiculosBloqueio");
        }
    }
}

