using Domain.ConsultaCNPJCPF;
using Domain.ConsultaCNPJCPF.InterfacesDTO;

namespace Infra.ConsultaCNPJCPF
{

    public class BuscaService : IBuscaService
    {
        // Dependências do serviço
        private readonly IComprasService _comprasService;
        private readonly IDividasService _dividasService;
        private readonly IFazendaService _fazendaService;
        private readonly IDetranService _detranService;
        private readonly IDueService _dueService;
        private readonly IGovernoEstadualService _governoEstadualService;
        private readonly IGovernoFederalService _governoFederalService;
        private readonly IRHService _rhService;
        private readonly ITCEService _tceService;
        private readonly ITCUService _tcuService;
        private readonly ITSEService _tseService;

        public BuscaService(IComprasService comprasService,
                            IDividasService dividasService,
                            IFazendaService fazendaService,
                            IDetranService detranService,
                            IDueService dueService,
                            IGovernoEstadualService governoEstadualService,
                            IGovernoFederalService governoFederalService,
                            IRHService rhService,
                            ITCEService tceService,
                            ITCUService tcuService,
                            ITSEService tseService)
        {
            _comprasService = comprasService;
            _dividasService = dividasService;
            _fazendaService = fazendaService;
            _detranService = detranService;
            _dueService = dueService;
            _governoEstadualService = governoEstadualService;
            _governoFederalService = governoFederalService;
            _rhService = rhService;
            _tceService = tceService;
            _tcuService = tcuService;
            _tseService = tseService;
        }

        public async IAsyncEnumerable<object> BuscarCNPJ(string cnpj, List<string> tabelasSelecionadas)
        {
            if (tabelasSelecionadas.Contains("todas") || !tabelasSelecionadas.Any())
            {
                tabelasSelecionadas = new List<string>
            {
                "compras", "dividas", "fazenda", "detran", "estadual", "federal", "tce", "tcu", "tse"
            };
            }

            if (tabelasSelecionadas.Contains("compras"))
            {
                var compras = await _comprasService.BuscarCNPJ(cnpj);
                if (compras != null)
                {
                    yield return new { Compras = compras };
                }
            }

            if (tabelasSelecionadas.Contains("dividas"))
            {
                var dividas = await _dividasService.BuscarCNPJ(cnpj);
                if (dividas != null)
                {
                    yield return new { Dividas = dividas };
                }
            }

            if (tabelasSelecionadas.Contains("fazenda"))
            {
                var fazenda = await _fazendaService.BuscarCNPJ(cnpj);
                if (fazenda != null)
                {
                    yield return new { Fazenda = fazenda };
                }
            }

            if (tabelasSelecionadas.Contains("detran"))
            {
                var detran = await _detranService.BuscarCNPJ(cnpj);
                if (detran != null)
                {
                    yield return new { Detran = detran };
                }
            }

            if (tabelasSelecionadas.Contains("estadual"))
            {
                var estadual = await _governoEstadualService.BuscarCNPJ(cnpj);
                if (estadual != null)
                {
                    yield return new { Estadual = estadual };
                }
            }

            if (tabelasSelecionadas.Contains("federal"))
            {
                var federal = await _governoFederalService.BuscarCNPJ(cnpj);
                if (federal != null)
                {
                    yield return new { Federal = federal };
                }
            }

            if (tabelasSelecionadas.Contains("tce"))
            {
                var tce = await _tceService.BuscaCNPJ(cnpj);
                if (tce != null)
                {
                    yield return new { TCE = tce };
                }
            }

            if (tabelasSelecionadas.Contains("tcu"))
            {
                var tcu = await _tcuService.BuscaCNPJ(cnpj);
                if (tcu != null)
                {
                    yield return new { TCU = tcu };
                }
            }

            if (tabelasSelecionadas.Contains("tse"))
            {
                var tse = await _tseService.BuscaCNPJ(cnpj);
                if (tse != null)
                {
                    yield return new { TSE = tse };
                }
            }
        }

        public async IAsyncEnumerable<object> BuscarCPF(string cpf, List<string> tabelasSelecionadas)
        {
            if (tabelasSelecionadas.Contains("todas") || !tabelasSelecionadas.Any())
            {
                tabelasSelecionadas = new List<string>
            {
                "compras", "dividas", "fazenda", "detran", "due", "estadual", "federal", "tce", "tcu", "tse"
            };
            }

            if (tabelasSelecionadas.Contains("compras"))
            {
                var compras = await _comprasService.BuscarCPF(cpf);
                if (compras != null)
                {
                    yield return new { Compras = compras };
                }
            }

            if (tabelasSelecionadas.Contains("dividas"))
            {
                var dividas = await _dividasService.BuscarCPF(cpf);
                if (dividas != null)
                {
                    yield return new { Dividas = dividas };
                }
            }

            if (tabelasSelecionadas.Contains("fazenda"))
            {
                var fazenda = await _fazendaService.BuscarCPF(cpf);
                if (fazenda != null)
                {
                    yield return new { Fazenda = fazenda };
                }
            }

            if (tabelasSelecionadas.Contains("detran"))
            {
                var detran = await _detranService.BuscarCPF(cpf);
                if (detran != null)
                {
                    yield return new { Detran = detran };
                }
            }

            if (tabelasSelecionadas.Contains("due"))
            {
                var due = await _dueService.BuscarCPF(cpf);
                if (due != null)
                {
                    yield return new { Due = due };
                }
            }

            if (tabelasSelecionadas.Contains("estadual"))
            {
                var estadual = await _governoEstadualService.BuscarCPF(cpf);
                if (estadual != null)
                {
                    yield return new { Estadual = estadual };
                }
            }

            if (tabelasSelecionadas.Contains("federal"))
            {
                var federal = await _governoFederalService.BuscarCPF(cpf);
                if (federal != null)
                {
                    yield return new { Federal = federal };
                }
            }

            if (tabelasSelecionadas.Contains("tce"))
            {
                var tce = await _tceService.BuscarCPF(cpf);
                if (tce != null)
                {
                    yield return new { TCE = tce };
                }
            }

            if (tabelasSelecionadas.Contains("tcu"))
            {
                var tcu = await _tcuService.BuscarCPF(cpf);
                if (tcu != null)
                {
                    yield return new { TCU = tcu };
                }
            }

            if (tabelasSelecionadas.Contains("tse"))
            {
                var tse = await _tseService.BuscarCPF(cpf);
                if (tse != null)
                {
                    yield return new { TSE = tse };
                }
            }
        }
    }






}








