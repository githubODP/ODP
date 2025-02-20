
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ODP.Web.UI.Extensions;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Consultas.ResultadoConsulta;
using ODP.Web.UI.Models.Interfaces.Compras;
using ODP.Web.UI.Models.Interfaces.Detran;
using ODP.Web.UI.Models.Interfaces.Dividas;
using ODP.Web.UI.Models.Interfaces.Fazenda;
using ODP.Web.UI.Models.Interfaces.GovernoEstadual;
using ODP.Web.UI.Models.Interfaces.GovernoFederal;
using ODP.Web.UI.Models.Interfaces.RecursosHumanos;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCE;
using ODP.Web.UI.Models.Interfaces.Tribunal.TCU;
using ODP.Web.UI.Models.Interfaces.Tribunal.TSE;
using ODP.Web.UI.Services.Compras;
using ODP.Web.UI.Services.Consultas;
using ODP.Web.UI.Services.Consultas.Repositories.Compras;
using ODP.Web.UI.Services.Consultas.Repositories.Dividas;
using ODP.Web.UI.Services.Consultas.Repositories.Due;
using ODP.Web.UI.Services.Consultas.Repositories.Fazenda;
using ODP.Web.UI.Services.Consultas.Repositories.GovernoEstadual;
using ODP.Web.UI.Services.Consultas.Repositories.GovernoFederal;
using ODP.Web.UI.Services.Consultas.Repositories.Tribunal.TCE;
using ODP.Web.UI.Services.Consultas.Repositories.Tribunal.TCU;
using ODP.Web.UI.Services.Consultas.Repositories.Tribunal.TSE;
using ODP.Web.UI.Services.Cooperacao;
using ODP.Web.UI.Services.Corregedoria;
using ODP.Web.UI.Services.Demanda;
using ODP.Web.UI.Services.Detran;
using ODP.Web.UI.Services.Dividas;
using ODP.Web.UI.Services.DueDiligence;
using ODP.Web.UI.Services.Fazenda;
using ODP.Web.UI.Services.GovernoEstadual;
using ODP.Web.UI.Services.GovernoFederal;
using ODP.Web.UI.Services.Grafico;
using ODP.Web.UI.Services.Handlers;
using ODP.Web.UI.Services.Identidade;
using ODP.Web.UI.Services.Internos;
using ODP.Web.UI.Services.RecursosHumanos;
using ODP.Web.UI.Services.Tribunal.TCE;
using ODP.Web.UI.Services.Tribunal.TCU;
using ODP.Web.UI.Services.Tribunal.TSE;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace ODP.Web.UI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();




            //Interno - Due, auditoria, corregedoria e documentos
            services.AddHttpClient<IInstauracaoService, InstauracaoService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(10, TimeSpan.FromSeconds(60)));


            services.AddHttpClient<IDueService, DueService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IAnaliseService, AnaliseService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICooperacaoService, CooperacaoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.EsperarTentar())
                .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            services.AddHttpClient<IDemandaService, DemandaService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            /// Recursos Humanos 

            services.AddHttpClient<IFuncionarioService, FuncionarioService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IDependenteService, DependenteService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));



            /// Compras - Dispensas, Licitacões e Contratos

            services.AddHttpClient<IDispensaService, DispensaService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ILicitacaoService, LicitacaoService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IContratoService, ContratoService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            ///// Detran 
            ///
            services.AddHttpClient<IVeiculoBaixadoService, VeiculoBaixadoService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IVeiculoBloqueadoService, VeiculoBloqueadoService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IVeiculoCirculacaoService, VeiculoCirculacaoService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IVeiculoIndisponivelService, VeiculoIndisponivelService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IVeiculoOrdemJudicialService, VeiculoOrdemJudicialService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IVeiculoProntuarioService, VeiculoProntuarioService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IVeiculoRegistroForaService, VeiculoRegistroForaService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            // Dividas

            services.AddHttpClient<IDividaFGTsService, DividaFGTSService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IDividaNaoPrevidenciaService, DividaNaoPrevidenciaService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IDividaPrevidenciaService, DividaPrevidenciaService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            ///Fazenda
            ///
            services.AddHttpClient<IJuceparService, JuceparService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<INFEletronicaService, NFEletronicaService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<INFEletronicaFederalService, NFEletronicaFederalService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            ///Governo Estadual 

            services.AddHttpClient<IAmbientalService, AmbientalService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IPADVService, PADVService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            ///Governo Federal 
            ///

            services.AddHttpClient<IAcordoLenienciaService, AcordoLenienciaService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IAeronaveService, AeronaveService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            services.AddHttpClient<IBeneficioContinuoService, BeneficioContinuoService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICeisService, CeisService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IExpulsoFederalService, ExpulsoFederalService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IPEPService, PEPService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            services.AddHttpClient<ISeguroDefesoService, SeguroDefesoService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ITrabalhoEscravoService, TrabalhoEscravoService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IBolsaService, BolsaService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICepimService, CepimService>()
                 .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                 .AddPolicyHandler(PollyExtensions.EsperarTentar())
                 .AddTransientHttpErrorPolicy(
                 p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICnepService, CnepService>()
                 .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                 .AddPolicyHandler(PollyExtensions.EsperarTentar())
                 .AddTransientHttpErrorPolicy(
                 p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));





            //Tribunais

            //- TCU

            services.AddHttpClient<ICNPJRestritoService, CNPJRestritoService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICPFRestritoService, CPFRestritoService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            services.AddHttpClient<IInadimplenteService, InadimplenteService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IIrregularidadeService, IrregularidadeService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            //- TCE

            services.AddHttpClient<IContaEleitoralIrregularService, ContaEleitoralIrregularService>()
                 .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                 .AddPolicyHandler(PollyExtensions.EsperarTentar())
                 .AddTransientHttpErrorPolicy(
                 p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IContaIrregularService, ContaIrregularService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            services.AddHttpClient<IInabilitadoService, InabilitadoService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IInidoneoService, InidoneoService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            //- TSE


            services.AddHttpClient<IDoacaoCandidatoService, DoacaoCandidatoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.EsperarTentar())
                .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IDoacaoPartidoService, DoacaoPartidoService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));


            services.AddHttpClient<IDoacaoPartidoGeralService, DoacaoPartidoGeralService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IDoacaoGeralService, DoacaoGeralService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IFornecedorPartidoService, FornecedorPartidoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.EsperarTentar())
                .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IFornecedorCandidatoService, FornecedorCandidatoService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(PollyExtensions.EsperarTentar())
                   .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IFornecedorCandidatoService, FornecedorCandidatoService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));





            services.AddScoped<IUser, AspNetUser>();

            services.AddScoped<IBuscaService, BuscaService>();
            services.AddScoped<IComprasServicos, ComprasServicos>();
            services.AddScoped<IDividasServicos, DividasServicos>();
            services.AddScoped<IFazendaServicos, FazendaServicos>();
            services.AddScoped<IGovernoEstadualServicos, GovernoEstadualServicos>();
            services.AddScoped<IGovernoFederalServicos, GovernoFederalServicos>();
            services.AddScoped<ITCEServicos, TCEServicos>();
            services.AddScoped<ITCUServicos, TCUServicos>();
            services.AddScoped<ITSEServicos, TSEServicos>();
            services.AddScoped<IInternosServicos, InternoServicos>();



            //services.AddScoped<IDetranServicos, DetranServicos>();

            //graficos
            services.AddHttpClient<IGraficoService, GraficoService>()
                 .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                 .AddPolicyHandler(PollyExtensions.EsperarTentar())
                 .AddTransientHttpErrorPolicy(
                 p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));



            // Contratos Internos

            services.AddHttpClient<IContratosInternosService, ContratosInternosService>()
                  .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                  .AddPolicyHandler(PollyExtensions.EsperarTentar())
                  .AddTransientHttpErrorPolicy(
                  p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));







        }


        public class PollyExtensions
        {
            public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
            {
                var retry = HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(new[]
                    {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    }, (outcome, timespan, retryCount, context) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Tentando pela {retryCount} vez!");
                        Console.ForegroundColor = ConsoleColor.White;
                    });

                return retry;
            }
        }



    }
}
