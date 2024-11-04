using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Identidade.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Observatorio Identity API",
                    Description = "Esta API faz parte do Observatorio Applications.",
                    Contact = new OpenApiContact() { Name = "Rivalter Silva", Email = "rivaltersilva@cge.pr.gov.br" },
                    License = new OpenApiLicense() { Name = "ODP", Url = new Uri("https://www.cge.pr.gov.br") }
                });

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}
