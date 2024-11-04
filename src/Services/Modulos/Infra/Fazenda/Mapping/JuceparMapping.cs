using Domain.Fazenda.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ODP.Parana.API.Models.Mapping
{
    public class JuceparMapping : IEntityTypeConfiguration<Jucepar>
    {
        public void Configure(EntityTypeBuilder<Jucepar> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.CNPJ)
                .IsRequired(false)
                .HasColumnType("varchar(18)");

            builder.Property(c => c.CPF)
                .IsRequired(false)
                .HasColumnType("varchar(15)");

            builder.Property(c => c.RazaoSocial)
                .IsRequired(false)
                .HasColumnType("varchar(170)");

            builder.Property(c => c.Endereco)
                .IsRequired(false)
                .HasColumnType("varchar(70)");

            builder.Property(c => c.Nro)
                .IsRequired(false)
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Complemento)
                .IsRequired(false)
                .HasColumnType("varchar(60)");

            builder.Property(c => c.Bairro)
                .IsRequired(false)
                .HasColumnType("varchar(40)");

            builder.Property(c => c.CEP)
                .IsRequired(false)
                .HasColumnType("varchar(10)");

            builder.Property(c => c.NIRE)
                .IsRequired(false)
                .HasColumnType("varchar(15)");

            builder.Property(c => c.MEI)
                .IsRequired(false)
                .HasColumnType("varchar(3)");

            builder.Property(c => c.NomesSocio)
                .IsRequired(false)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Vinculo)
                .IsRequired(false)
                .HasColumnType("varchar(140)");

            builder.Property(c => c.Situacao)
                .IsRequired(false)
                .HasColumnType("varchar(40)");


            builder.Property(c => c.EntradaSociedade)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.Property(c => c.SaidaSociedade)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.Property(c => c.InicioMandato)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.Property(c => c.TerminoMandato)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.Property(c => c.CapitalSocial)
                .IsRequired(false)
                .HasColumnType("varchar(25)");

            builder.Property(c => c.Constituicao)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.Property(c => c.InicioAtividade)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.Property(c => c.TerminoAtividade)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.ToTable("JuntaComercial");
        }
    }
}
