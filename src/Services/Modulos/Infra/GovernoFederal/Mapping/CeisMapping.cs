
using Domain.GovernoFederal.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.GovernoFederal.Mapping
{
    public class CEISMapping : IEntityTypeConfiguration<Ceis>
    {
        public void Configure(EntityTypeBuilder<Ceis> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.Cadastro)
                    .IsRequired()
                    .HasColumnType("varchar(4)");

            builder.Property(c => c.Codigo)
                    .IsRequired()
                    .HasColumnType("int");

            builder.Property(c => c.TipoPessoa)
                .HasColumnType("varchar(1)");

            builder.Property(c => c.CNPJCPF)
                .IsRequired()
                .HasColumnType("varchar(18)");

            builder.Property(c => c.NomeInformadoOrgaoSancionador)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(c => c.RazaoSocialReceita)
                .IsRequired(false)
                .HasColumnType("varchar(140)");

            builder.Property(c => c.NomeFantasiaReceita)
                .IsRequired(false)
                .HasColumnType("varchar(140)");

            builder.Property(c => c.NroProcesso)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(c => c.TipoSancao)
                .IsRequired()
                .HasColumnType("varchar(160)");

            builder.Property(c => c.DTInicioSancao)
                .HasColumnType("DateTime");

            builder.Property(c => c.DTFimSancao)
                .HasColumnType("DateTime");

            builder.Property(c => c.OrgaoSancionador)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.UFOrgaoSancionador)
                .HasColumnType("varchar(2)");

            builder.Property(c => c.DTPublicacao)
                .HasColumnType("DateTime");

            builder.Property(c => c.Publicacao)
                .HasColumnType("varchar(90)");

            builder.Property(c => c.Detalhamento)
                .IsRequired(false)
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.AbrangenciaDecisao)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.Property(c => c.FundamentacaoLegal)
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.DTTransitoJulgado)
                .HasColumnType("DateTime");

            builder.ToTable("CEIS");
        }
    }
}
