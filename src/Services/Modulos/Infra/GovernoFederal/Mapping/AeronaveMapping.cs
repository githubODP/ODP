
using Domain.GovernoFederal.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.GovernoFederal.Mapping
{
    public class AeronaveMapping : IEntityTypeConfiguration<Aeronave>
    {
        public void Configure(EntityTypeBuilder<Aeronave> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Marca)
               .IsRequired()
               .HasColumnType("varchar(5)");

            builder.Property(c => c.Proprietario)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.OutrosProprietarios)
               .IsRequired()
               .HasColumnType("varchar(500)");

            builder.Property(c => c.SGUF)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.CNPJCPF)
                .IsRequired()
                .HasColumnType("varchar(18)");

            builder.Property(c => c.Operador)
                .IsRequired()
                .HasColumnType("varchar(130)");

            builder.Property(c => c.OutrosOperadores)
               .IsRequired()
               .HasColumnType("varchar(500)");

            builder.Property(c => c.UFOperador)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.CPFCGC)
                .IsRequired()
                .HasColumnType("varchar(18)");

            builder.Property(c => c.Matricula)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(c => c.NroSerie)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(c => c.Categoria)
               .IsRequired()
               .HasColumnType("varchar(5)");

            builder.Property(c => c.CodTipo)
               .IsRequired()
               .HasColumnType("varchar(15)");

            builder.Property(c => c.Modelo)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(c => c.Fabricante)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(c => c.CDInterdicao)
                .IsRequired()
                .HasColumnType("varchar(7)");

            builder.Property(c => c.MarcaNacional)
                .IsRequired()
                .HasColumnType("varchar(7)");

            builder.Property(c => c.DescricaoGravame)
                .IsRequired()
                .HasColumnType("varchar(600)");

            builder.ToTable("Aeronaves");
        }
    }
}