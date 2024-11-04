using CGEODP.Core.Data;
using Domain.GovernoEstadual.Entidades;

namespace Domain.GovernoEstadual.Interfaces
{
    public interface IAmbientalRepositoryRead : IRepositoryRead<Ambiental>, IBuscaInfo<Ambiental>
    {
    }
}
