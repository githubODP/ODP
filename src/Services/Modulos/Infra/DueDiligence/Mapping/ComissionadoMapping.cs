
using Domain.DueDiligence.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infra.DueDiligence.Mapping
{
    public class ComissionadoMapping : IEntityTypeConfiguration<Comissionado>
    {
        public void Configure(EntityTypeBuilder<Comissionado> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.NroProtocolo)
                .IsRequired()
                .HasColumnType("varchar(12)");

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(c => c.Orgao)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(c => c.Indicacao)
            .IsRequired()
            .HasColumnType("varchar(30)");

            builder.Property(c => c.Responsavel)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.DataSolicitacao)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.DataResposta)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.ClassificacaoRisco)
            .IsRequired()
            .HasColumnType("int");

            builder.Property(c => c.Observacao)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Evidencias)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Decreto)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.CadEmpresario)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CadEmpresaContrato)
               .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadEmpresaInidoneos)
               .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadInelegivel)
              .IsRequired()
              .HasColumnType("bit");

            builder.Property(c => c.CadExpulsoFederal)
              .IsRequired()
              .HasColumnType("bit");

            builder.Property(c => c.CadMEI)
                .IsRequired()
               .HasColumnType("bit");


            builder.Property(c => c.CadAuxilio)
                .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadPPP)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CadPEP)
                .IsRequired()
               .HasColumnType("bit");


            builder.Property(c => c.CadPPN)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CadVinculoDireto)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CadPrisao)
              .IsRequired()
              .HasColumnType("bit");

            builder.Property(c => c.CadDoacaoEleitoral)
               .IsRequired()
               .HasColumnType("bit");


            builder.Property(c => c.CadDoacaoEleitoralPartido)
                .IsRequired()
                .HasColumnType("bit");


            builder.Property(c => c.CadDoacaoEleitoralCandidato)
                .IsRequired()
                .HasColumnType("bit");


            builder.Property(c => c.CadFornecedorPartido)
                .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadFornecedorCandidato)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CadFiliacaoPartido)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CadQuadroInabilitados)
                 .IsRequired()
                 .HasColumnType("bit");

            builder.Property(c => c.CadOFAC)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CadInterpol)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CadCVM)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CadInabilitado)
              .IsRequired()
             .HasColumnType("bit");

            builder.Property(c => c.CadInidoneo)
               .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadFuncionarioPublico)
                .IsRequired()
                .HasColumnType("bit");

            builder.Property(c => c.CadAposentado)
                .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadImprobidade)
               .IsRequired()
              .HasColumnType("bit");            

            builder.Property(c => c.CadTrabalhoEscravo)
               .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadDebitoPGFN)
               .IsRequired()
               .HasColumnType("bit");


            builder.Property(c => c.CadRestricoesTCE)
               .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadInadimplentesTCE)
               .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadIrregularidadesTCE)
               .IsRequired()
               .HasColumnType("bit");


            builder.Property(c => c.CadIrregularTCU)
               .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadIrregularEleitoralTCU)
               .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadParentescoGrauServidorPublico)
               .IsRequired()
               .HasColumnType("bit");


            builder.Property(c => c.CadCepim)
              .IsRequired()
              .HasColumnType("bit");

            builder.Property(c => c.CadCnep)
               .IsRequired()
               .HasColumnType("bit");

            builder.Property(c => c.CadAcordo)
               .IsRequired()
               .HasColumnType("bit");



            builder.ToTable("Comissionados");
        }
    }
}
