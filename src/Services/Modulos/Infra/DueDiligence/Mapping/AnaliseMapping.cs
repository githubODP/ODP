using Domain.DueDiligence.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.DueDiligence.Mapping
{
    public class AnaliseMapping : IEntityTypeConfiguration<Analise>
    {
        public void Configure(EntityTypeBuilder<Analise> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(c => c.NroProtocolo)
               .HasColumnType("varchar(12)");

            builder.Property(c => c.DataAnalise)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.AnaliseTecnica)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Risco)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.Property(c => c.Ressalvas)
               .IsRequired()
               .HasColumnType("varchar(30)");

            builder.Property(x => x.Responsavel)
                .IsRequired()
                .HasColumnType("nvarchar(150)");


            // Configuração do relacionamento com Comissionado
            builder.HasOne(a => a.Comissionado)
                .WithMany() // Um Comissionado pode ter várias Análises
                .HasForeignKey(a => a.ComissionadoId)
                .OnDelete(DeleteBehavior.Cascade); // Excluir análises ao excluir comissionado


            builder.ToTable("Analises");
        }
    }
}
