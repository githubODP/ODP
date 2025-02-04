using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.TermoCooperacao.Mapping
{
    public class InstauracaoMapping : IEntityTypeConfiguration<Domain.TermoCooperacao.Entidade.TermoCooperacao>
    {
        public void Configure(EntityTypeBuilder<Domain.TermoCooperacao.Entidade.TermoCooperacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Celebrante)
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Celebrado)
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Objeto)
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.DataCelebracao)
              .HasColumnType("Datetime");

            builder.Property(c => c.InicioVigencia)
              .HasColumnType("Datetime");

            builder.Property(c => c.FimVIgencia)
              .HasColumnType("Datetime");

            builder.ToTable("TermoCooperacao");
        }
    }
}
