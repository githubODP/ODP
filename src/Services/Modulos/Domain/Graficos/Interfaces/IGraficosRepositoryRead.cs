using Domain.Graficos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Graficos.Interfaces
{
   public interface IGraficosRepositoryRead
    {
        public Task<Grafico> GraficoOdp();

    }
}
