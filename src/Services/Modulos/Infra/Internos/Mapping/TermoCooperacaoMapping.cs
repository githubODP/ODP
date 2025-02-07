using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Internos.Mapping
{
    public class TermoMapping : IEntityTypeConfiguration<Domain.Internos.Entidade.TermoCooperacao>
    {
        public void Configure(EntityTypeBuilder<Domain.Internos.Entidade.TermoCooperacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Protocolo)
               .HasColumnType("varchar(15)");

            builder.Property(c => c.Orgao)
               .HasColumnType("varchar(50)");

            builder.Property(c => c.Sigla)
              .HasColumnType("varchar(10)");

            builder.Property(c => c.NroTermo)
               .HasColumnType("varchar(20)");

            builder.Property(c => c.InicioVigencia)
             .HasColumnType("Datetime");

            builder.Property(c => c.FimVIgencia)
              .HasColumnType("Datetime");

            builder.Property(c => c.Validade)
               .HasColumnType("int");

            builder.Property(m => m.Ativo)
              .HasColumnType("bit");

            builder.Property(c => c.Status)
               .HasColumnType("varchar(30)");

            builder.Property(m => m.Renovar)
             .HasColumnType("bit");

            builder.Property(c => c.DIOE)
              .HasColumnType("int");

            builder.Property(c => c.DataPublicacao)
              .HasColumnType("Datetime");

            builder.Property(c => c.Objeto)
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Regulamentacao)
              .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Informacoes)
               .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Observacao)
              .HasColumnType("nvarchar(max)");

            builder.ToTable("TermosCooperacao");
        }
    }
}
