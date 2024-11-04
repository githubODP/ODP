using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Consultas.ResultadoConsulta;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas
{


    public class BuscaService : IBuscaService
    {
        private readonly IComprasServicos _comprasServicos;
        private readonly IDividasServicos _dividasServicos;
        private readonly IFazendaServicos _fazendaServicos;
        private readonly IGovernoEstadualServicos _governoEstadualServicos;
        private readonly IGovernoFederalServicos _governoFederalServicos;
        private readonly ITCUServicos _tcuServicos;
        private readonly ITCEServicos _tceServicos;
        private readonly ITSEServicos _tseServicos;
        private readonly IInternosServicos _internoServicos;

        public BuscaService(IComprasServicos comprasServicos,
                            IDividasServicos dividasServicos,
                            IFazendaServicos fazendaServicos,
                            IGovernoEstadualServicos governoEstadualServicos,
                            IGovernoFederalServicos governoFederalServicos,
                            ITCUServicos tcuServicos,
                            ITCEServicos tceServicos,
                            ITSEServicos tseServicos,
                            IInternosServicos internoServicos)
        {
            _comprasServicos = comprasServicos;
            _dividasServicos = dividasServicos;
            _fazendaServicos = fazendaServicos;
            _governoEstadualServicos = governoEstadualServicos;
            _governoFederalServicos = governoFederalServicos;
            _tcuServicos = tcuServicos;
            _tceServicos = tceServicos;
            _tseServicos = tseServicos;
            _internoServicos = internoServicos;
        }

        // Método utilitário para executar ações com tratamento de erro
        private async Task<T> ExecutarComTratamentoDeErro<T>(Func<Task<T>> acao)
        {
            try
            {
                return await acao();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar ação: {ex.Message}");
                return default;
            }
        }

        // Métodos de busca individual
        public async Task<ResultadoDTO> BuscarCNPJ(string cnpj)
        {
            var resultado = new ResultadoDTO
            {

                BuscaCompras = await ExecutarComTratamentoDeErro(() => _comprasServicos.BuscarCNPJ(cnpj)),
                BuscaDividas = await ExecutarComTratamentoDeErro(() => _dividasServicos.BuscarCNPJ(cnpj)),
                BuscaFazenda = await ExecutarComTratamentoDeErro(() => _fazendaServicos.BuscarCNPJ(cnpj)),
                BuscaEstadual = await ExecutarComTratamentoDeErro(() => _governoEstadualServicos.BuscarCNPJ(cnpj)),
                BuscaFederal = await ExecutarComTratamentoDeErro(() => _governoFederalServicos.BuscarCNPJ(cnpj)),
                BuscaTCU = await ExecutarComTratamentoDeErro(() => _tcuServicos.BuscarCNPJ(cnpj)),
                BuscaTCE = await ExecutarComTratamentoDeErro(() => _tceServicos.BuscarCNPJ(cnpj)),
                BuscaTSE = await ExecutarComTratamentoDeErro(() => _tseServicos.BuscarCNPJ(cnpj)),
                BuscaInterno = await ExecutarComTratamentoDeErro(() => _internoServicos.BuscarCNPJ(cnpj)),

            };

            return resultado;
        }

        public async Task<ResultadoDTO> BuscarCPF(string cpf)
        {
            var resultado = new ResultadoDTO
            {

                BuscaCompras = await ExecutarComTratamentoDeErro(() => _comprasServicos.BuscarCPF(cpf)),
                BuscaDividas = await ExecutarComTratamentoDeErro(() => _dividasServicos.BuscarCPF(cpf)),
                BuscaFazenda = await ExecutarComTratamentoDeErro(() => _fazendaServicos.BuscarCPF(cpf)),
                BuscaEstadual = await ExecutarComTratamentoDeErro(() => _governoEstadualServicos.BuscarCPF(cpf)),
                BuscaFederal = await ExecutarComTratamentoDeErro(() => _governoFederalServicos.BuscarCPF(cpf)),
                BuscaTCU = await ExecutarComTratamentoDeErro(() => _tcuServicos.BuscarCPF(cpf)),
                BuscaTCE = await ExecutarComTratamentoDeErro(() => _tceServicos.BuscarCPF(cpf)),
                BuscaTSE = await ExecutarComTratamentoDeErro(() => _tseServicos.BuscarCPF(cpf)),
                BuscaInterno = await ExecutarComTratamentoDeErro(() => _internoServicos.BuscarCPF(cpf))
            };

            return resultado;
        }




        public async Task<ResultadoDTO> BuscarCNPJPorTabelasSelecionadas(string cnpj, List<string> tabelasSelecionadas)
        {
            var resultado = new ResultadoDTO();

            if (tabelasSelecionadas.Contains("compras"))
                resultado.BuscaCompras = await _comprasServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("dividas"))
                resultado.BuscaDividas = await _dividasServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("fazenda"))
                resultado.BuscaFazenda = await _fazendaServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("estadual"))
                resultado.BuscaEstadual = await _governoEstadualServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("federal"))
                resultado.BuscaFederal = await _governoFederalServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("tce"))
                resultado.BuscaTCE = await _tceServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("tcu"))
                resultado.BuscaTCU = await _tcuServicos.BuscarCNPJ(cnpj);

            if (tabelasSelecionadas.Contains("tse"))
                resultado.BuscaTSE = await _tseServicos.BuscarCNPJ(cnpj);

            

            return resultado;
        }


        public async Task<ResultadoDTO> BuscarCPFPorTabelasSelecionadas(string cpf, List<string> tabelasSelecionadas)
        {
            var resultado = new ResultadoDTO();

            if (tabelasSelecionadas.Contains("compras"))
                resultado.BuscaCompras = await _comprasServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("dividas"))
                resultado.BuscaDividas = await _dividasServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("fazenda"))
                resultado.BuscaFazenda = await _fazendaServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("estadual"))
                resultado.BuscaEstadual = await _governoEstadualServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("federal"))
                resultado.BuscaFederal = await _governoFederalServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("tce"))
                resultado.BuscaTCE = await _tceServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("tcu"))
                resultado.BuscaTCU = await _tcuServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("tse"))
                resultado.BuscaTSE = await _tseServicos.BuscarCPF(cpf);

            if (tabelasSelecionadas.Contains("interno"))
                resultado.BuscaInterno = await _internoServicos.BuscarCPF(cpf);

            return resultado;
        }

        /// <summary> para cima esta funcionando. 
        /// 


        public async Task<List<ResultadoDTO>> BuscarEmLoteAsync(List<string> identificadores, bool isCNPJ, List<string> tabelasSelecionadas)
        {
            var resultados = new List<ResultadoDTO>();

            foreach (var id in identificadores)
            {
                ResultadoDTO resultado;

                if (isCNPJ)
                {
                    resultado = await BuscarCNPJPorTabelasSelecionadas(id, tabelasSelecionadas);
                }
                else
                {
                    resultado = await BuscarCPFPorTabelasSelecionadas(id, tabelasSelecionadas);
                }

                resultados.Add(resultado);
            }

            return resultados;
        }






    }



}
