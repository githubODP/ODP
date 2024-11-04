using CGEODP.Core.Data;
using Domain.Corregedoria.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Corregedoria.Interfaces
{
    public interface IVideoTranscricaoRepositoryRead 
    {
        Task<VideoExtracao> AddVideoAsync(VideoExtracao video);
        Task<VideoExtracao> GetVideoAsync(Guid id);
        Task UpdateTranscriptionAsync(Guid id, string transcription);
    }
}
