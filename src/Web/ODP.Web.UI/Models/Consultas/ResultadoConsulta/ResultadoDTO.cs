
using ODP.Web.UI.Models.Consultas.DTOViewModels.Compras;
using ODP.Web.UI.Models.Consultas.DTOViewModels.Dividas;
using ODP.Web.UI.Models.Consultas.DTOViewModels.Fazenda;
using ODP.Web.UI.Models.Consultas.DTOViewModels.GovernoEstadual;
using ODP.Web.UI.Models.Consultas.DTOViewModels.GovernoFederal;
using ODP.Web.UI.Models.Consultas.DTOViewModels.Internos;
using ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TCE;
using ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TCU;
using ODP.Web.UI.Models.Consultas.DTOViewModels.Tribunais.TSE;



namespace ODP.Web.UI.Models.Consultas.ResultadoConsulta
{
    public class ResultadoDTO
    {

        public BuscaComprasDTO BuscaCompras { get; set; }
        public BuscaDividasDTO BuscaDividas { get; set; }
        public BuscaFazendaDTO BuscaFazenda { get; set; }
        public BuscaEstadualDTO BuscaEstadual { get; set; }
        public BuscaFederalDTO BuscaFederal { get; set; }
        public BuscaTCUDTO BuscaTCU { get; set; }
        public BuscaTCEDTO BuscaTCE { get; set; }
        public BuscaTSEDTO BuscaTSE { get; set; }
        public BuscaInternoDTO BuscaInterno { get; set; }





    }
}
