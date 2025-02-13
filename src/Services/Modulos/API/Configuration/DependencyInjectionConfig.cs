
using Detran.Infra.RepositoriesRead;
using Dividas.Domain.Interfaces;
using Dividas.Infra.Repositories;
using Domain.Compras.Interfaces;
using Domain.ConsultaCNPJCPF;
using Domain.ConsultaCNPJCPF.InterfacesDTO;
using Domain.Corregedoria.Interfaces;
using Domain.Detran.Interfaces;
using Domain.DueDiligence.Interfaces;
using Domain.Fazenda.Interfaces;
using Domain.GovernoEstadual.Interfaces;
using Domain.GovernoFederal.Interfaces;
using Domain.Graficos.Interfaces;
using Domain.Internos.Interfaces;
using Domain.Library.Interfaces;
using Domain.RecursosHumanos.Interfaces.RepositoriesRead;
using Domain.Tribunal.Interfaces.TCE;
using Domain.Tribunal.Interfaces.TCU;
using Domain.Tribunal.Interfaces.TSE;
using Infra.Compras.RepositoriesRead;
using Infra.ConsultaCNPJCPF;
using Infra.ConsultaCNPJCPF.Repositories.Compras;
using Infra.ConsultaCNPJCPF.Repositories.Detran;
using Infra.ConsultaCNPJCPF.Repositories.Dividas;
using Infra.ConsultaCNPJCPF.Repositories.Due;
using Infra.ConsultaCNPJCPF.Repositories.Fazenda;
using Infra.ConsultaCNPJCPF.Repositories.GovernoEstadual;
using Infra.ConsultaCNPJCPF.Repositories.GovernoFederal;
using Infra.ConsultaCNPJCPF.Repositories.RH;
using Infra.ConsultaCNPJCPF.Repositories.Tribunal;
using Infra.Corregedoria.Repositories;
using Infra.Corregedoria.RepositoriesRead;
using Infra.Data;
using Infra.DueDiligence.Repositories;
using Infra.DueDiligence.RepositoriesRead;
using Infra.Fazenda.RepositoriesRead;
using Infra.GovernoEstadual.Repositories;
using Infra.GovernoFederal.Repositories;
using Infra.Graficos.RepositoryRead;
using Infra.Internos.Repositories;
using Infra.Internos.RepositoriesRead;
using Infra.RecursosHumanos.RepositoriesRead;
using Infra.Tribunal.Repository.TCE;
using Infra.Tribunal.Repository.TCU;
using Infra.Tribunal.Repository.TSE;
using Library.Infra.Repositories;
using Library.Infra.RepositoriesRead;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            


            //Corregedoria
            services.AddScoped<IInstauracaoRepository, InstauracaoRepository>();
            services.AddScoped<IInstauracaoRepositoryRead, InstauracaoRepositoryRead>();


            ///Due Diligence             
            services.AddScoped<IDueRepository, DueRepository>();
            services.AddScoped<IDueRepositoryRead, DueRepositoryRead>();

            //Analise Tecnica
            services.AddScoped<IAnaliseRepository, AnaliseRepository>();
            services.AddScoped<IAnaliseRepositoryRead, AnaliseRepositoryRead>();


            ///Compras
            services.AddScoped<IContratoRepositoryRead, ContratoRepositoryRead>();
            services.AddScoped<IDispensaRepositoryRead, DispensaRepositoryRead>();
            services.AddScoped<ILicitacaoRepositoryRead, LicitacaoRepositoryRead>();


            //Dividas Federais - FGTS ; Dividas Não Previdenciarias; Dividas Previdenciarias
            services.AddScoped<IDividaFgtsRepositoryRead, DividaFgtsRepositoryRead>();
            services.AddScoped<IDividaNaoPrevidenciaRepositoryRead, DividaNaoPrevidenciariaRepositoryRead>();
            services.AddScoped<IDividaPrevidenciaRepositoryRead, DividaPrevidenciariaRepositoryRead>();


            ///Fazenda Publica 

            services.AddScoped<INFEletronicaRepositoryRead, NFEletronicaRepositoryRead>();
            services.AddScoped<IJuceparRepositoryRead, JuceparRepositoryRead>();
            services.AddScoped<INFEletronicaFederalRepositoryRead, NFEletronicaFederalRepositoryRead>();

            //Governo Estadual

            services.AddScoped<IAmbientalRepositoryRead, AmbientalRepositoryRead>();
            services.AddScoped<IPADVRepositoryRead, PADVRepositoryRead>();

            ////Governo Federal

            services.AddScoped<IAeronaveRepositoryRead, AeronaveRepositoryRead>();
            services.AddScoped<IBeneficioContinuoRepositoryRead, BeneficioContinuoRepositoryRead>();
            services.AddScoped<ICeisRepositoryRead, CeisRepositoryRead>();
            services.AddScoped<IExpulsoFederalRepositoryRead, ExpulsoFederalRepositoryRead>();
            services.AddScoped<ISeguroDefesoRepositoryRead, SeguroDefesoRepositoryRead>();
            services.AddScoped<ITrabalhoEscravoRepositoryRead, TrabalhoEscravoRepositoryRead>();
            services.AddScoped<IAcordoLenienciaRepositoryRead, AcordoLenienciaRepositoryRead>();
            services.AddScoped<IPEPRepositoryRead, PEPRepositoryRead>();
            services.AddScoped<ICnepRepositoryRead, CnepRepositoryRead>();
            services.AddScoped<IBolsaFamiliaRepositoryRead, BolsaFamiliaRepositoryRead>();
            services.AddScoped<ICepimRepositoryRead, CepimRepositoryRead>();


            ///Documentos Internos   

            services.AddScoped<IDocumentoInternoRepository, DocumentoInternoRepository>();
            services.AddScoped<IDocumentoInternoRepositoryRead, DocumentoInternoRepositoryRead>();
            services.AddScoped<IDemandaRepositoryRead, DemandaRepositoryRead>();

            ////Recursos Humanos    

            services.AddScoped<IDependenteRepositoryRead, DependenteRepositoryRead>();
            services.AddScoped<IFuncionarioRepositoryRead, FuncionarioRepositoryRead>();
            services.AddScoped<IOcorrenciaRepositoryRead, OcorrenciaRepositoryRead>();
            services.AddScoped<IRubricaRepositoryRead, RubricaRepositoryRead>();



            ///Detran

            services.AddScoped<IVeiculoBaixadoRepositoryRead, VeiculoBaixadoRepositoryRead>();
            services.AddScoped<IVeiculoBloqueadoRepositoryRead, VeiculoBloqueadoRepositoryRead>();
            services.AddScoped<IVeiculoCirculacaoRepositoryRead, VeiculoCirculacaoRepositoryRead>();
            services.AddScoped<IVeiculoIndisponivelRepositoryRead, VeiculoIndisponivelRepositoryRead>();
            services.AddScoped<IVeiculoOrdemJudicialRepositoryRead, VeiculoOrdemJudicialRepositoryRead>();
            services.AddScoped<IVeiculoProntuarioRepositoryRead, VeiculoProntuarioRepositoryRead>();
            services.AddScoped<IVeiculoRegistroForaRepositoryRead, VeiculoRegistroForaRepositoryRead>();


            ///Tribunais de Conta - TCE/PR e Tribunal de Conta Uniao - TCU

            services.AddScoped<IInidoneoRepositoryRead, InidoneoRepositoryRead>();
            services.AddScoped<IInabilitadoRepositoryRead, InabilitadoRepositoryRead>();
            services.AddScoped<ICNPJRestritoRepositoryRead, CNPJRestritoRepositoryRead>();
            services.AddScoped<ICPFRestritoRepositoryRead, CPFRestritoRepositoryRead>();
            services.AddScoped<IInadimplenteRepositoryRead, InadimplenteRepositoryRead>();
            services.AddScoped<IIrregularidadeRepositoryRead, IrregularidadeRepositoryRead>();
            services.AddScoped<IContaEleitoralIrregularRepositoryRead, ContaEleitoralIrregularRepositoryRead>();
            services.AddScoped<IContaIrregularRepositoryRead, ContaIrregularRepositoryRead>();


            //Consultas CNPJ e CPF
            services.AddScoped<ITSEService, TSEService>();
            services.AddScoped<ITCEService, TCEService>();
            services.AddScoped<ITCUService, TCUService>();
            services.AddScoped<IFazendaService, FazendaService>();
            services.AddScoped<IComprasService, ComprasService>();
            services.AddScoped<IDetranService, DetranService>();
            services.AddScoped<IDividasService, DividasService>();
            services.AddScoped<IGovernoEstadualService, GovernoEstadualService>();
            services.AddScoped<IGovernoFederalService, GovernoFederalService>();
            services.AddScoped<IDueService, DueService>();
            services.AddScoped<IRHService, RHService>();


            /// TSE

            services.AddScoped<IDoacaoCandidatoRepositoryRead, DoacaoCandidatoRepositoryRead>();
            services.AddScoped<IDoacaoGeralRepositoryRead, DoacaoGeralRepositoryRead>();
            services.AddScoped<IDoacaoPartidoGeralRepositoryRead, DoacaoPartidoGeralRepositoryRead>();
            services.AddScoped<IDoacaoPartidoRepositoryRead, DoacaoPartidoRepositoryRead>();
            services.AddScoped<IFornecedorCandidatoRepositoryRead, FornecedorCandidatoRepositoryRead>();
            services.AddScoped<IFornecedorPartidoRepositoryRead, FornecedorPartidoRepositoryRead>();

            //Resultado 
            services.AddScoped<IBuscaService, BuscaService>();




            //Contexto
            services.AddScoped<ObservatorioContext>();


            //Graficos

            services.AddScoped<IGraficosRepositoryRead, GraficoRepositoryRead>();

            //Termo de Cooperação

            services.AddScoped<ITermoCooperacaoRepositoryRead, TermoCooperacaoRepositoryRead>();
            services.AddScoped<ITermoCooperacaoRepository, TermoCooperacaoRepository>();



        }
    }
}
