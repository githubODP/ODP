using ODP.Web.UI.Models.Graficos;
using System.Threading.Tasks;

namespace ODP.Web.UI.Services.Grafico
{
    public interface IGraficoService
    {
        Task<GraficoViewModel> GraficoODP();
    }
}
