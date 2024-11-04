using Domain.RecursosHumanos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.RecursosHumanos.Mapping
{
    public class OcorrenciaMapping : IEntityTypeConfiguration<Ocorrencia>
    {
        public void Configure(EntityTypeBuilder<Ocorrencia> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CPF)
             .IsRequired()
             .HasColumnType("varchar(15)");

            builder.Property(c => c.Codigo)
             .IsRequired()
             .HasColumnType("varchar(10)");

            builder.Property(c => c.DescricaoOcorrencia)
             .IsRequired()
             .HasColumnType("varchar(120)");

            builder.Property(c => c.InicioLicenca)
             .IsRequired()
             .HasColumnType("DateTime");

            builder.Property(c => c.MesInicio)
             .IsRequired()
             .HasColumnType("int");

            builder.Property(c => c.AnoInicio)
             .IsRequired()
             .HasColumnType("int");

            builder.Property(c => c.FimLicenca)
             .IsRequired()
             .HasColumnType("DateTime");

            builder.Property(c => c.MesFim)
             .IsRequired()
             .HasColumnType("int");

            builder.Property(c => c.AnoFim)
             .IsRequired()
             .HasColumnType("int");

            builder.Property(c => c.Tempo)
             .IsRequired()
             .HasColumnType("int");

            builder.Property(c => c.TotalTempoLicenca)
             .IsRequired()
             .HasColumnType("int");

            builder.Property(c => c.Orgao)
             .IsRequired()
             .HasColumnType("varchar(10)");

            builder.Property(c => c.Ordinal)
             .IsRequired()
             .HasColumnType("int");

            builder.Property(c => c.Matricula)
             .IsRequired()
             .HasColumnType("varchar(10)");

            builder.Property(c => c.IDUnico)
             .IsRequired()
             .HasColumnType("varchar(10)");

            // Relacionamento muitos-para-muitos com Funcionario
            builder.HasMany(r => r.FuncionarioOcorrencias)
                   .WithOne(fr => fr.Ocorrencia)
                   .HasForeignKey(fr => fr.OcorrenciaId);

            builder.ToTable("Ocorrencias");
        }

    }
}
