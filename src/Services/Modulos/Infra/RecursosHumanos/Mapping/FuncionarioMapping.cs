

using Domain.RecursosHumanos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.RecursosHumanos.Mapping
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Matricula)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(70)");

            builder.Property(c => c.CPF)
                .IsRequired(false)
                .HasColumnType("varchar(15)");

            builder.Property(c => c.RG)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(c => c.DtNascimento)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.Idade)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.NomeMae)
                .IsRequired(false)
                .HasColumnType("varchar(70)");

            builder.Property(c => c.Orgao)
                .IsRequired(false)
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Ano)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Mes)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.TipoRH)
                .IsRequired(false)
                .HasColumnType("varchar(40)");

            builder.Property(c => c.Quadro)
                .IsRequired(false)
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Lotacao)
                .IsRequired(false)
                .HasColumnType("varchar(70)");

            builder.Property(c => c.Cargo)
                .IsRequired(false)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Simbolo)
                .IsRequired(false)
                .HasColumnType("varchar(15)");

            builder.Property(c => c.Inicio)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.DescricaoInicio)
                .IsRequired(false)
                .HasColumnType("varchar(70)");

            builder.Property(c => c.Desligamento)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.TempoServico)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.CargaHoraria)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.IdOrigem)
                .IsRequired(false)
                .HasColumnType("varchar(15)");

            builder.Property(c => c.OrdinalOrigem)
                .IsRequired(false)
                .HasColumnType("varchar(15)");

            builder.Property(c => c.IDUnico)
                .IsRequired(false)
                .HasColumnType("varchar(10)");

            builder.Property(c => c.FGP)
                .IsRequired(false)
                .HasColumnType("varchar(15)");

            builder.Property(c => c.Funcao)
                .IsRequired(false)
                .HasColumnType("varchar(80)");

            //// Relacionamento muitos-para-muitos com Rubrica
            //builder.HasMany(f => f.FuncionarioRubricas)
            //       .WithOne(fr => fr.Funcionario)
            //       .HasForeignKey(fr => fr.FuncionarioId);


            // Relacionamento muitos-para-muitos com Ocorrencia
            builder.HasMany(f => f.Dependentes)
            .WithOne(d => d.Funcionario)
            .HasForeignKey(d => d.FuncionarioId);



            // Relacionamento muitos-para-muitos com Rubrica
            //builder.HasMany(f => f.FuncionarioOcorrencias)
            //       .WithOne(fr => fr.Funcionario)
            //       .HasForeignKey(fr => fr.FuncionarioId);

            //builder.ToTable("Funcionarios");
        }
    }


}
