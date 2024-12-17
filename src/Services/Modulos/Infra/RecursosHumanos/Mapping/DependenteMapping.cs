using Domain.RecursosHumanos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.RecursosHumanos.Mapping
{
    public class DependenteMapping : IEntityTypeConfiguration<Dependente>
    {
        public void Configure(EntityTypeBuilder<Dependente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.IDUnico)
             .IsRequired()
             .HasColumnType("varchar(10)");

            builder.Property(c => c.Matricula)
             .IsRequired()
             .HasColumnType("varchar(8)");

            builder.Property(c => c.Ordinal)
             .IsRequired()
             .HasColumnType("varchar(3)");

            builder.Property(c => c.CPF)
             .IsRequired()
             .HasColumnType("varchar(15)");

            builder.Property(c => c.Orgao)
             .IsRequired()
             .HasColumnType("varchar(10)");

            builder.Property(c => c.MatriculaDependente)
             .IsRequired()
             .HasColumnType("varchar(8)");

            builder.Property(c => c.CPFDependente)
             .HasColumnType("varchar(15)");

            builder.Property(c => c.RGDependente)
             .HasColumnType("varchar(15)");

            builder.Property(c => c.NomeDependente)
             .IsRequired()
             .HasColumnType("varchar(80)");

            builder.Property(c => c.DTNascDependente)
             .IsRequired()
             .HasColumnType("DateTime");

            builder.Property(c => c.SexoDependente)
             .IsRequired()
             .HasColumnType("varchar(11)");

            builder.Property(c => c.SexoDependente)
             .IsRequired()
             .HasColumnType("varchar(10)");

            builder.Property(c => c.TipoDependente)
             .IsRequired()
             .HasColumnType("varchar(40)");

            builder.Property(c => c.FimDependente)
             .IsRequired()
             .HasColumnType("DateTime");

            builder.Property(c => c.IdadeDependente)
             .IsRequired()
             .HasColumnType("int");

            builder.Property(c => c.InicioDependente)
             .IsRequired()
             .HasColumnType("DateTime");

            // Relacionamento muitos-para-muitos com Funcionario
            builder.HasOne(d => d.Funcionario)
           .WithMany(f => f.Dependentes) // Funcionario tem muitos dependentes
           .HasForeignKey(d => d.FuncionarioId);

            builder.ToTable("Dependentes");
        }

    }
}
