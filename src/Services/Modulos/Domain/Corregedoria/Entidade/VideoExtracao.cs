using CGEODP.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Corregedoria.Entidade
{
    public class VideoExtracao : Entity, IAggregateRoot
    {
        
        public string FilePath { get; set; }
        public string Transcription { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
