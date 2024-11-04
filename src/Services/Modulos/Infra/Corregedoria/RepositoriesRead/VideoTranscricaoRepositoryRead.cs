using CGEODP.Core.DomainObjects;
using Domain.Corregedoria.Entidade;
using Domain.Corregedoria.Interfaces;
using Infra.Data;
using Infra.RepositoryExterno;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Corregedoria.RepositoriesRead
{
    public class VideoTranscricaoRepositoryRead : IVideoTranscricaoRepositoryRead
    {
        protected readonly ObservatorioContext _context;

        public VideoTranscricaoRepositoryRead(ObservatorioContext context)
        {
            _context = context;
        }



        public async Task<VideoExtracao> AddVideoAsync(VideoExtracao video)
        {
            _context.VideoExtracao.Add(video);
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task<VideoExtracao> GetVideoAsync(Guid id)
        {
            return await _context.VideoExtracao.FindAsync(id);
        }

        public async Task UpdateTranscriptionAsync(Guid id, string transcription)
        {
            var video = await _context.VideoExtracao.FindAsync(id);
            if (video != null)
            {
                video.Transcription = transcription;
                await _context.SaveChangesAsync();
            }
        }
    }
}
