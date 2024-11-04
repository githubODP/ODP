using Domain.RecursosHumanos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.RecursosHumanos.Mapping
{
    public class FuncionarioDependenteMapping : IEntityTypeConfiguration<FuncionarioDependente>
    {
        public void Configure(EntityTypeBuilder<FuncionarioDependente> builder)
        {
            builder.HasKey(fr => new { fr.FuncionarioId, fr.DependenteId });

            builder.HasOne(fr => fr.Funcionario)
                .WithMany(f => f.FuncionarioDependentes)
                .HasForeignKey(fr => fr.FuncionarioId);

            builder.HasOne(fr => fr.Dependente)
                .WithMany(r => r.FuncionarioDependentes)
                .HasForeignKey(fr => fr.DependenteId);

            builder.ToTable("FuncionarioDependentes");
        }
    }
}
