using CGEODP.Core.DomainObjects;

namespace Domain.Corregedoria.Entidade
{
    public class VideoExtracao : Entity, IAggregateRoot
    {

        public string FilePath { get; set; }
        public string Transcription { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
