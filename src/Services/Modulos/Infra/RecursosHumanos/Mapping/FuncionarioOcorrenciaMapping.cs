using Domain.RecursosHumanos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.RecursosHumanos.Mapping
{
    public class FuncionarioOcorrenciaMapping : IEntityTypeConfiguration<FuncionarioOcorrencia>
    {
        public void Configure(EntityTypeBuilder<FuncionarioOcorrencia> builder)
        {
            builder.HasKey(fr => new { fr.FuncionarioId, fr.OcorrenciaId });

            builder.HasOne(fr => fr.Funcionario)
                .WithMany(f => f.FuncionarioOcorrencias)
                .HasForeignKey(fr => fr.FuncionarioId);

            builder.HasOne(fr => fr.Ocorrencia)
                .WithMany(r => r.FuncionarioOcorrencias)
                .HasForeignKey(fr => fr.OcorrenciaId);

            builder.ToTable("FuncionarioOcorrencias");
        }
    }
}
