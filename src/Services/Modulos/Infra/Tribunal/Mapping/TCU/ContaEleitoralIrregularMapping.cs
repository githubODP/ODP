using Domain.Tribunal.Entidades.TCU;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Tribunal.Mapping.TCU
{
    public class ContaEleitoralIrregularMapping : IEntityTypeConfiguration<ContaEleitoralIrregular>
    {
        public void Configure(EntityTypeBuilder<ContaEleitoralIrregular> builder)
        {
            builder.HasKey(x => x.Id);



            builder.Property(c => c.Nome)
              .IsRequired()
              .HasColumnType("varchar(150)");


            builder.Property(c => c.CPF)
             .IsRequired()
             .HasColumnType("varchar(15)");

            builder.Property(c => c.CargoFuncao)
             .IsRequired()
             .HasColumnType("varchar(200)");


            builder.Property(c => c.Processo)
            .IsRequired()
            .HasColumnType("varchar(18)");


            builder.Property(c => c.Municipio)
             .IsRequired()
             .HasColumnType("varchar(100)");

            builder.Property(c => c.UF)
             .IsRequired()
             .HasColumnType("varchar(02)");

            builder.Property(c => c.Deliberacao)
             .IsRequired()
             .HasColumnType("varchar(18)");

            builder.Property(c => c.DataJulgado)
             .IsRequired()
             .HasColumnType("DateTime");

            builder.Property(c => c.DataFinal)
             .IsRequired()
             .HasColumnType("DateTime");


            builder.ToTable("ContasEleitoraisIrregulares");
        }
    }
}
