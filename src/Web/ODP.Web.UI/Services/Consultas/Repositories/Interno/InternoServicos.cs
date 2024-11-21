using ODP.Web.UI.Models.Consultas.DTOViewModels.Internos;
using ODP.Web.UI.Models.Consultas.InterfaceDTO;
using ODP.Web.UI.Services.Demanda;
using ODP.Web.UI.Services.DueDiligence;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Consultas.Repositories.Due
{
    public class InternoServicos : IInternosServicos
    {
        private readonly IDueService _dueService;
        private readonly IDemandaService _demandaService;

        public InternoServicos(IDueService dueService, IDemandaService demandaService)
        {
            _dueService = dueService;
            _demandaService = demandaService;
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



            return new BuscaInternoDTO
            {

                DueDiligence = due,
                Demanda = demanda,

            };
        }
    }
}
