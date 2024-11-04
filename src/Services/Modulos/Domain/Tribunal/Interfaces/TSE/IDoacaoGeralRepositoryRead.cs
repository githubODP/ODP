using CGEODP.Core.Data;
using Domain.Tribunal.Entidades.TSE;

namespace Domain.Tribunal.Interfaces.TSE
{
    public interface IDoacaoGeralRepositoryRead : IRepositoryRead<DoacaoGeral>, IBuscaInfo<DoacaoGeral>
    {
    }
}
