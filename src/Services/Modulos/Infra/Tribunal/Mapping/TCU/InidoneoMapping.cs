using Domain.Tribunal.Entidades.TCU;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TCU
{
    public class InidoneoMapping : IEntityTypeConfiguration<Inidoneo>
    {
        public void Configure(EntityTypeBuilder<Inidoneo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

            builder.Property(c => c.CNPJ)
            .IsRequired()
            .HasColumnType("varchar(18)");

            builder.Property(c => c.UF)            
            .HasColumnType("varchar(2)");

            builder.Property(c => c.Processo)
            .IsRequired()
            .HasColumnType("varchar(15)");

            builder.Property(c => c.Deliberacao)
            .IsRequired()
            .HasColumnType("varchar(20)");

            builder.Property(c => c.TransitoJulgado)            
            .HasColumnType("DateTime");

            builder.Property(c => c.DataFinal)        
            .HasColumnType("DateTime");

            builder.Property(c => c.DataAcordao)         
            .HasColumnType("DateTime");

            builder.ToTable("Inidoneos");
        }
    }
}
