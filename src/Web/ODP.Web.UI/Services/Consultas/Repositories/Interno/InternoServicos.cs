using ODP.Web.UI.Models.Consultas.DTOViewModels.Internos;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Models.Interfaces.RecursosHumanos;
using ODP.Web.UI.Services.Demanda;
using ODP.Web.UI.Services.DueDiligence;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas.Repositories.Due
{
    public class InternoServicos : IInternosServicos
    {
        private readonly IDueService _dueService;
        private readonly IDemandaService _demandaService;
        private readonly IFuncionarioService _funcionarioService;
        private readonly IDependenteService _dependenteService;

        public InternoServicos(IDueService dueService, IDemandaService demandaService, IDependenteService dependenteService, IFuncionarioService funcionarioService)
        {
            _dueService = dueService;
            _demandaService = demandaService;
            _funcionarioService = funcionarioService;
            _dependenteService = dependenteService;
        }


        public async Task<BuscaInternoDTO> BuscarCNPJ(string cnpj)
        {

            var demanda = await _demandaService.BuscarCNPJ(cnpj);



            return new BuscaInternoDTO
            {


                Demanda = demanda,

            };
        }


        public async Task<BuscaInternoDTO> BuscarCPF(string cpf)
        {
            var due = await _dueService.BuscarPorCPF(cpf);
            var demanda = await _demandaService.BuscarCPF(cpf);
            var dependente = await _dependenteService.BuscarCPF(cpf);
            var funcionario = await _funcionarioService.BuscarCPF(cpf);



            return new BuscaInternoDTO
            {

                DueDiligence = due,
                Demanda = demanda,
                Dependente = dependente,
                Funcionario = funcionario

            };
        }
    }
}
