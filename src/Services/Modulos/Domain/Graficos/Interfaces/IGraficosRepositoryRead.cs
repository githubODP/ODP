using Domain.Graficos.Entidades;

namespace Domain.Graficos.Interfaces
{
    public interface IGraficosRepositoryRead
    {
        public Task<Grafico> GraficoOdp();

    }
}
