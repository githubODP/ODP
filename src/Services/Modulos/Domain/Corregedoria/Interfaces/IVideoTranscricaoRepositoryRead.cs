using Domain.Corregedoria.Entidade;

namespace Domain.Corregedoria.Interfaces
{
    public interface IVideoTranscricaoRepositoryRead
    {
        Task<VideoExtracao> AddVideoAsync(VideoExtracao video);
        Task<VideoExtracao> GetVideoAsync(Guid id);
        Task UpdateTranscriptionAsync(Guid id, string transcription);
    }
}
