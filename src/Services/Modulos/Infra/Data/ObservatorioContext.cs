using CGEODP.Core.Data;
using Dividas.Domain.Entidades;
using Domain.Compras.Entidades;
using Domain.Corregedoria.Entidade;
using Domain.Detran.Entidades;
using Domain.DueDiligence.Entidade;
using Domain.Fazenda.Entidades;
using Domain.GovernoEstadual.Entidades;
using Domain.GovernoFederal.Entidades;
using Domain.Internos.Entidade;
using Domain.Library.Entidades;
using Domain.RecursosHumanos.Entidades;
using Domain.Tribunal.Entidades.TCE;
using Domain.Tribunal.Entidades.TCU;
using Domain.Tribunal.Entidades.TSE;
using Infra.RecursosHumanos.Mapping;
using Microsoft.EntityFrameworkCore;


namespace Infra.Data
{
    public class ObservatorioContext : DbContext, IUnitOfWork
    {
        public ObservatorioContext(DbContextOptions<ObservatorioContext> options) : base(options) { }


        /// Documentos Internos
        public DbSet<DocumentoInterno> DocumentosInternos { get; set; }
        public DbSet<DemandasInternas> Demandas {  get; set; }  


        ////Corregedoria
        public DbSet<Instauracao> Instauracoes { get; set; }
        public DbSet<VideoExtracao> VideoExtracao { get; set; }


        /// Due Diligence      
        public DbSet<Comissionado> Comissionados { get; set; }


        // Compras
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Dispensa> Dispensas { get; set; }
        public DbSet<Licitacao> Licitacoes { get; set; }


        //Dividas
        public DbSet<DividaFGTS> DividasFGTS { get; set; }
        public DbSet<DividaNaoPrevidenciaria> DividasNaoPrevidenciarias { get; set; }
        public DbSet<DividaPrevidenciaria> DividasPrevidenciarias { get; set; }


        // Tribunais
        public DbSet<Inabilitado> Inabilitados { get; set; }
        public DbSet<Inidoneo> Inidoneos { get; set; }
        public DbSet<CNPJRestrito> CNPJRestritos { get; set; }
        public DbSet<CPFRestrito> CPFRestritos { get; set; }
        public DbSet<Inadimplente> Inadimplentes { get; set; }
        public DbSet<Irregularidade> Irregularidades { get; set; }
        public DbSet<ContaEleitoralIrregular> ContasEleitoraisIrregulares { get; set; }
        public DbSet<ContaIrregular> ContasIrregulares { get; set; }



        /// Fazenda  

        public DbSet<NFEletronica> NFEletronicas { get; set; }
        public DbSet<Jucepar> JuntaComercial { get; set; }
        public DbSet<NFEletronicaFederal> NFEletronicasFederais { get; set; }


        /// Governo Federal 

        public DbSet<Aeronave> Aeronaves { get; set; }
        public DbSet<BeneficioContinuo> BeneficiosContinuos { get; set; }
        public DbSet<Ceis> Ceis { get; set; }
        public DbSet<ExpulsoFederal> ExpulsosFederais { get; set; }
        public DbSet<SeguroDefeso> SeguroDefeso { get; set; }
        public DbSet<TrabalhoEscravo> TrabalhoEscravo { get; set; }
        public DbSet<AcordoLeniencia> AcordosLeniencia { get; set; }
        public DbSet<PEP> PEPS { get; set; }
        public DbSet<Cnep> CNEP { get; set; }
        public DbSet<BolsaFamilia> BolsaFamilias { get; set; }
        public DbSet<Cepim> Cepim { get; set; }

        //Governo Estadual 

        public DbSet<Ambiental> Ambientais { get; set; }
        public DbSet<PADV> PADVS { get; set; }



        /// Detran
        public DbSet<VeiculoCirculacao> VeiculosCirculacao { get; set; }
        public DbSet<VeiculoBaixado> VeiculosBaixados { get; set; }
        public DbSet<VeiculoRegistroFora> VeiculosRegistradoFora { get; set; }
        public DbSet<VeiculoOrdemJudicial> VeiculosJudiciais { get; set; }
        public DbSet<VeiculoIndispAdmin> VeiculosIndisponivel { get; set; }
        public DbSet<VeiculoProntuario> VeiculosProntuario { get; set; }
        public DbSet<VeiculoBloqueioRoubo> VeiculosBloqueio { get; set; }



        /// <summary>
        /// Recursos Humanos

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Dependente> Dependentes { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<Rubrica> Rubricas { get; set; }
        public DbSet<FuncionarioRubrica> FuncionarioRubricas { get; set; }
        public DbSet<FuncionarioRubrica> FuncionarioDependentes { get; set; }
        public DbSet<FuncionarioRubrica> FuncionarioOcorrencias { get; set; }


        //TSE
        public DbSet<DoacaoCandidato> DoacoesCandidatos { get; set; }
        public DbSet<DoacaoPartido> DoacoesPartidos { get; set; }
        public DbSet<DoacaoGeral> DoacoesGerais { get; set; }
        public DbSet<DoacaoPartidoGeral> DoacoesPartidosGerais { get; set; }
        public DbSet<FornecedorPartido> FornecedorPartidos { get; set; }
        public DbSet<FornecedorCandidato> FornecedorCandidatos { get; set; }



        /// <summary>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FuncionarioMapping());
            modelBuilder.ApplyConfiguration(new RubricaMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioRubricaMapping());

            modelBuilder.ApplyConfiguration(new FuncionarioMapping());
            modelBuilder.ApplyConfiguration(new OcorrenciaMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioOcorrenciaMapping());

            modelBuilder.ApplyConfiguration(new FuncionarioMapping());
            modelBuilder.ApplyConfiguration(new DependenteMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioDependenteMapping());

            base.OnModelCreating(modelBuilder);

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ObservatorioContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }

}
