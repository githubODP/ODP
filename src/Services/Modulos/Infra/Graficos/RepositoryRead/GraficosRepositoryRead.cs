using CGEODP.Core.DomainObjects;
using Domain.DueDiligence.Entidade;
using Domain.Graficos.Entidades;
using Domain.Graficos.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infra.Graficos.RepositoryRead
{
    public class GraficoRepositoryRead : IGraficosRepositoryRead, IAggregateRoot
    {
        protected readonly ObservatorioContext _context;
        public GraficoRepositoryRead(ObservatorioContext context)
        {
            _context = context;
        }


        public async Task<Grafico> GraficoOdp()
        {
            var dados = await _context.Set<Comissionado>()
                .AsNoTracking()
                .GroupBy(c => c.Orgao)
                .Select(g => new
                {
                    orgao = g.Key,
                    quantidade = g.Count()
                })
                .ToListAsync();


            var content = JsonSerializer.Serialize(dados);



            return new Grafico
            {
                Content = content
            };
        }

    }
}
