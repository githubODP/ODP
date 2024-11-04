using Domain.RecursosHumanos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.RecursosHumanos.Mapping
{
    public class FuncionarioRubricaMapping : IEntityTypeConfiguration<FuncionarioRubrica>
    {
        public void Configure(EntityTypeBuilder<FuncionarioRubrica> builder)
        {
            builder.HasKey(fr => new { fr.FuncionarioId, fr.RubricaId });

            builder.HasOne(fr => fr.Funcionario)
                .WithMany(f => f.FuncionarioRubricas)
                .HasForeignKey(fr => fr.FuncionarioId);

            builder.HasOne(fr => fr.Rubrica)
                .WithMany(r => r.FuncionarioRubricas)
                .HasForeignKey(fr => fr.RubricaId);

            builder.ToTable("FuncionarioRubricas");
        }
    }
}
