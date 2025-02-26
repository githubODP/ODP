
using CGEODP.WebApi.Core.Identidade;
using Domain.Internos.Entidade;
using Domain.Internos.Interfaces;
using Infra.Data;
using Infra.Internos.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;


namespace API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ObservatorioContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                 sqlOptions => sqlOptions.CommandTimeout(180))); // Timeout set to 180 seconds));
            services.AddControllers()

             .AddJsonOptions(options =>
              {
                  options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantém os nomes das propriedades
                  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Enum como string
              });
            

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            // Configuração do serviço de e-mail
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
