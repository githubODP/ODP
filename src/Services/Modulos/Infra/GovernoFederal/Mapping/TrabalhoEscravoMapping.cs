
using Domain.GovernoFederal.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.GovernoFederal.Mapping
{
    public class TrabalhoEscravoMapping : IEntityTypeConfiguration<TrabalhoEscravo>
    {
        public void Configure(EntityTypeBuilder<TrabalhoEscravo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Ano)
                .HasColumnType("int");

            builder.Property(c => c.UF)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.Property(c => c.Empregador)
                .IsRequired()
                .HasColumnType("varchar(90)");

            builder.Property(c => c.CNPJCPF)
                .IsRequired()
                .HasColumnType("varchar(18)");

            builder.Property(c => c.Estabelecimento)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.TrabalhadoresEnvolvidos)
                .HasColumnType("int");

            builder.Property(c => c.CNAE)
                .HasColumnType("varchar(15)");

            builder.Property(c => c.DTDecisaoAdm)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.DTInclusao)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.ToTable("TrabalhoEscravo");
        }
    }
}
