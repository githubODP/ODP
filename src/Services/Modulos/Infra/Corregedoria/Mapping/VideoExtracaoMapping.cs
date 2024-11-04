using Domain.Corregedoria.Entidade;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Corregedoria.Mapping
{
    public class VideoExtracaoMapping : IEntityTypeConfiguration<VideoExtracao>
    {


        public void Configure(EntityTypeBuilder<VideoExtracao> builder)
        {
            builder.HasKey(x => x.Id);


            builder.Property(c => c.FilePath)
            .HasColumnType("nvarchar(max)") // Tipo de dado para uma string longa
            .IsRequired(); // Indica que é obrigatório

            builder.Property(c => c.Transcription)
                .HasColumnType("nvarchar(max)"); // Tipo de dado para uma string longa

            builder.Property(c => c.UploadedAt)
                .HasColumnType("datetime") // Tipo de dado para uma data e hora
                .IsRequired(); // Indica que é obrigatório



            builder.ToTable("VideoExtracao");
        }

    }
}
